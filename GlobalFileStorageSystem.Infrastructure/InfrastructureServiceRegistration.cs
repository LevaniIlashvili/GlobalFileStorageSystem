using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Authentication;
using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Infrastructure.Options;
using GlobalFileStorageSystem.Infrastructure.Persistance;
using GlobalFileStorageSystem.Infrastructure.Repositories;
using GlobalFileStorageSystem.Infrastructure.Services;
using GlobalFileStorageSystem.Infrastructure.Services.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using Npgsql;
using StackExchange.Redis;

namespace GlobalFileStorageSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection")));

            services.Configure<MinioOptions>(configuration.GetSection("Minio"));

            services.AddSingleton(sp =>
            {
                var minioSettings = sp.GetRequiredService<IOptions<MinioOptions>>().Value;

                return new MinioClient()
                    .WithEndpoint(minioSettings.Endpoint.Replace("http://", "").Replace("https://", ""))
                    .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey)
                    .Build();
            });

            services.AddSingleton<IMinioService, MinioService>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            });

            services.Configure<SmtpOptions>(configuration.GetSection("SmtpSettings"));

            services.AddScoped<IEmailService, EmailService>();

            services.AddSingleton<IPasswordGenerator, PasswordGenerator>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IRefreshTokenService, RedisRefreshTokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            return services;
        }
    }
}
