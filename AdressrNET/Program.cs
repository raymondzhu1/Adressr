using Microsoft.EntityFrameworkCore;
using Adressr.Data;
using Adressr.Data.Repository.Interface;
using Adressr.Data.Repository;
using AdressrNET.ExceptionHandlers;
using Adressr.Library;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Adressr.Service.Interface;
using Adressr.Service;
using AdressrNET;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//CORS configuration section.
string[] allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicies.Frontend, policy =>
    {
        //policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        policy.WithOrigins(allowedOrigins).WithHeaders("Content-Type", "Authorization").WithMethods("GET", "POST", "PUT", "DELETE").SetPreflightMaxAge(TimeSpan.FromHours(1));
    });
});

//probably would add some kind of logging service here. But since this is a simple app, I'm just gonna use a simple ILogger for now and just output it to console.

builder.Services.AddDbContext<AdressrContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Adressr")));

//Repo configuration
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPreferenceRepository, PreferenceRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
builder.Services.AddScoped<IProfileInterestRepository, ProfileInterestRepository>();
builder.Services.AddScoped<IProfileHobbyRepository, ProfileHobbyRepository>();
builder.Services.AddScoped<IOpeningRepository, OpeningRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IRepositorySaver, RepositorySaver>();

//Service layer configuration
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPreferenceService, PreferenceService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IOpeningService, OpeningService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IWorkTaskService, WorkTaskService>();

//Exception Handlers configured and triggered in order stated.
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ForbiddenExceptionHandler>();
builder.Services.AddExceptionHandler<DbUpdateExceptionHandler>();
builder.Services.AddExceptionHandler<UsernameExistsExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Add JWT Bearer configurations.
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors(CorsPolicies.Frontend);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();