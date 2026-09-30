using Adressr.Data.Repository.Interface;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Adressr.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AdressrContext _context;

        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int DegreeOfParallelism = 1;
        private const int Iterations = 2;
        private const int MemorySizeKiB = 19 * 1024;

        public UserRepository(AdressrContext context)
        {
            _context = context;
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
            return Base64Strings.PadRight(Base64Strings.Length + (4 -  Base64Strings.Length % 4) % 4, '=');
        }

        private static PHCParseResult ParsePHCString(string PHC)
        {

            //Add a check for the PHC string being given.
            string[] parts = PHC.Split('$', StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> parameters = parts[2].Split(',').Select(pars => pars.Split('=')).ToDictionary(ParamAndValues => ParamAndValues[0], ParamAndValues => int.Parse(ParamAndValues[1]));

            byte[] PassTheSalt = Convert.FromBase64String(PadBase64(parts[3]));
            byte[] PassTheHash = Convert.FromBase64String(PadBase64(parts[4]));

            PHCParseResult result = new PHCParseResult
            {
                Salt = PassTheSalt,
                Argon2idHash = PassTheHash,
                MemorySizeKiB = parameters["m"],
                Iterations = parameters["t"],
                DegreeOfParallelism = parameters["p"],
                Argon2Type = parts[0],
                Argon2Version = parts[1].Split('=')[1]
            };

            return result;
        }

        private class PHCParseResult
        {
            public required byte[] Salt { get; set; }
            public required byte[] Argon2idHash { get; set; }
            public int MemorySizeKiB { get; set; }
            public int Iterations { get; set; }
            public int DegreeOfParallelism { get; set; }
            public required string Argon2Type { get; set; }
            public required string Argon2Version { get; set; }
        }
    }
}
