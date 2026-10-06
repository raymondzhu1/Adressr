using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Library.CustomExceptions;
using Adressr.Data.Model;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IRepositorySaver _repositorySaver;
        private const int SaltSize = 16;
        private const int HashSize = 16;
        private const int DegreeOfParallelism = 1;
        private const int Iterations = 2;
        private const int MemorySizeKiB = 19 * 1024;
        private static readonly Dictionary<string, HashSet<int>> SupportedArgon2Stuff = new Dictionary<string, HashSet<int>>
        {
            ["argon2id"] = new HashSet<int> { 19 }
        };

        public UserService(IUserRepository userRepository, IPreferenceRepository preferenceRepository, IRepositorySaver repositorySaver)
        {
            _userRepository = userRepository;
            _preferenceRepository = preferenceRepository;
            _repositorySaver = repositorySaver;
        }

        private static byte[] GenerateSaltValue(int size)
        {
            return RandomNumberGenerator.GetBytes(size);
        }

        private static byte[] ComputeArgon2idHash(string password, byte[] salt, int parallelism = DegreeOfParallelism, int iteration = Iterations, int MemSize = MemorySizeKiB, int OutputLength = HashSize)
        {
            byte[] PasswordBytes = Encoding.UTF8.GetBytes(password);

            using Argon2id argon2 = new Argon2id(PasswordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = parallelism,
                Iterations = iteration,
                MemorySize = MemSize
            };

            return argon2.GetBytes(OutputLength);
        }

        private static string BuildPHCString(string argonType, string argonVersion, byte[] salt, byte[] argon2idHash, int memSize = MemorySizeKiB, int iteration = Iterations, int parallelism = DegreeOfParallelism)
        {
            string Base64Salt = Convert.ToBase64String(salt).TrimEnd('=');
            string Base64Argon2idHash = Convert.ToBase64String(argon2idHash).TrimEnd('=');

            return $"${argonType}$v={argonVersion}$m={memSize},t={iteration},p={parallelism}${Base64Salt}${Base64Argon2idHash}"; //argon2id and v=19 for now.
        }

        private static string PadBase64(string Base64Strings)
        {
            return Base64Strings.PadRight(Base64Strings.Length + (4 - Base64Strings.Length % 4) % 4, '=');
        }

        private static PHCParseResult ParsePHCString(string PHC)
        {
            if (string.IsNullOrEmpty(PHC))
            {
                throw new FormatException("The PHC string was null or empty.");
            }

            string[] parts = PHC.Split('$', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 5)
            {
                throw new FormatException("The PHC string didn't get segmented into 5 different parts.");
            }

            Dictionary<string, int> parameters = parts[2].Split(',').Select(pars => pars.Split('=')).ToDictionary(ParamAndValues => ParamAndValues[0], ParamAndValues => int.Parse(ParamAndValues[1]));

            if (parameters.Count != 3)
            {
                throw new FormatException("PHC string missing one of the cost parameters.");
            }

            if(!parameters.TryGetValue("m", out int memsizekib))
            {
                throw new FormatException("PHC string missing the m value.");
            }
            if(!parameters.TryGetValue("t", out int iterations))
            {
                throw new FormatException("PHC string missing the t value.");
            }
            if(!parameters.TryGetValue("p", out int parallelism))
            {
                throw new FormatException("PHC string missing the p value");
            }

            string[] versionParts = parts[1].Split('=');

            if (versionParts.Length != 2 || versionParts[0] != "v")
            {
                throw new FormatException("The version format of the PHC string got messed up.");
            }

            byte[] PassTheSalt = Convert.FromBase64String(PadBase64(parts[3]));
            byte[] PassTheHash = Convert.FromBase64String(PadBase64(parts[4]));

            PHCParseResult result = new PHCParseResult
            {
                Salt = PassTheSalt,
                Argon2idHash = PassTheHash,
                MemorySizeKiB = memsizekib,
                Iterations = iterations,
                DegreeOfParallelism = parallelism,
                Argon2Type = parts[0],
                Argon2Version = int.Parse(versionParts[1])
            };

            return result;
        }

        private static bool IsThisSupported(string argon2Type, int argon2Version)
        {
            return SupportedArgon2Stuff.TryGetValue(argon2Type, out var versions) && versions.Contains(argon2Version);
        }

        private class PHCParseResult
        {
            public required byte[] Salt { get; set; }
            public required byte[] Argon2idHash { get; set; }
            public int MemorySizeKiB { get; set; }
            public int Iterations { get; set; }
            public int DegreeOfParallelism { get; set; }
            public required string Argon2Type { get; set; }
            public int Argon2Version { get; set; }
        }

        public async Task<UserDTO> RegisterAsync(RegisterUserRequest request)
        {
            if(await _userRepository.GetByUsernameAsync(request.Username) != null)
            {
                throw new UsernameExistsException(request.Username);
            }

            if (await _userRepository.GetByEmailAsync(request.Email) != null)
            {
                throw new UsernameExistsException(request.Email);
            }

            byte[] salt = GenerateSaltValue(SaltSize);
            byte[] hash = ComputeArgon2idHash(request.Password, salt);
            string PHCString = BuildPHCString("argon2id", "19", salt, hash);

            User user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = PHCString
            };

            await _userRepository.AddUserAsync(user);

            Preference preference = new Preference { User = user, DarkMode = false, ProfilePrivate = false };
            await _preferenceRepository.AddAsync(preference);

            //add profile creation after this as well.

            await _repositorySaver.SaveChangesAsync();

            UserDTO result = new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email
            };

            return result;
        }

        public async Task<bool> VerifyIdentityAsync(string SomeIdentifier, string InputtedPassword)
        {
            User? user = await _userRepository.GetByUsernameOrEmailAsync(SomeIdentifier);
            if(user == null)
            {
                return false;
            }

            PHCParseResult ParsedResults = ParsePHCString(user.Password);
            if(!IsThisSupported(ParsedResults.Argon2Type, ParsedResults.Argon2Version))
            {
                //throw some exception saying this type and version ain't supported here.
            }

            byte[] UserLoginAttemptHash = ComputeArgon2idHash(InputtedPassword, ParsedResults.Salt, ParsedResults.DegreeOfParallelism, ParsedResults.Iterations, ParsedResults.MemorySizeKiB);
            return CryptographicOperations.FixedTimeEquals(UserLoginAttemptHash, ParsedResults.Argon2idHash);
        }
    }
}
