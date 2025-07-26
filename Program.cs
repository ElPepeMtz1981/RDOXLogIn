
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace RDOXMES.Login
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("By SoftwarechidoMx");
            Console.WriteLine();
            Console.WriteLine("Start Program.cs");

            var jwtKey = "RDOXMES_ClaveJWT_SuperSegura_2025_Produccion_ClaveExtra";
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration["Jwt:Key"] = jwtKey;
            builder.Configuration["Jwt:Issuer"] = "JwtLoginApi";
            builder.Configuration
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            var connection = builder.Configuration.GetConnectionString("Users");
            if (string.IsNullOrWhiteSpace(connection) || connection.Contains("USE_ENV_VARIABLE"))
            {
                builder.Logging.AddConsole();
                Console.WriteLine("connection string not loaded");
            }
            else
            {
                Console.WriteLine($"conections string loaded: {connection.Split(';')[0]}");
            }

            builder.Services.AddDbContext<ViewUserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Users")));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        RoleClaimType = ClaimTypes.Role
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Token JWT in format: Bearer {token}"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
                                               
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(5002); // o cualquier puerto libre
            });

            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
