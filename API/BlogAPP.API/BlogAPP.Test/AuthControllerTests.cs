using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebAPP.API.Controllers;
using WebAPP.API.Repositories.Interface;
using Moq;
using WebAPP.API.Models.DTO.RequestDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebAPP.API.Data;
using Microsoft.EntityFrameworkCore;
using WebAPP.API.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using WebAPP.API.Services.Interfaces;
using WebAPP.API.Models.ServiceModels;
using WebAPP.API.Services.Implementations;
using System.Text.Json;

namespace BlogAPP.Test
{
    public class AuthControllerTests : DependencySetupFixture
    {
        [Fact]
        public async Task Register_unsuccessful_email_already_taken()
        {
            //Arrange
            using var scope = ServiceProvider.CreateScope();

            var dataProtectionProvider = scope.ServiceProvider.GetService<IDataProtectionProvider>();
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var tokenRepository = scope.ServiceProvider.GetService<ITokenRepository>();
            var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
            var emailService = scope.ServiceProvider.GetService<IEmailService>();

            if (userManager == null || tokenRepository == null || userRepository == null || dataProtectionProvider == null || emailService == null)
                throw new ArgumentNullException("Service is null");

            var authController = new AuthController(userManager, tokenRepository, userRepository, emailService);

            var registerRequestDto = new RegisterRequestDto
            {
                Email = "demetrio.test@email.com",
                Password = "Demetrio1234!",
            };

            var contextMock = new Mock<HttpContext>();

            //Act
            if (userManager == null || tokenRepository == null || userRepository == null || dataProtectionProvider == null)
                throw new ArgumentNullException("Service is null");

            authController.ControllerContext.HttpContext = contextMock.Object;

            //Act
            var actionResult = await authController.Register(registerRequestDto);

            //Assert
            Assert.Equal(((BadRequestObjectResult)actionResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);

            string expectedMessage = $"Username \'{registerRequestDto.Email}\' is already taken.";

            var testResult = "";

            if (((BadRequestObjectResult)actionResult).Value is SerializableError errors)
            {
                foreach (var error in errors)
                {
                    var key = error.Key;
                    var value = error.Value;

                    if (value is string[] messages)
                    {
                        foreach (var message in messages)
                        {
                            if (message.Equals(expectedMessage))
                            {
                                testResult = message;
                                break;
                            }
                        }
                    }
                }
            }

            Assert.Equal(testResult, expectedMessage);
        }
    }

    // ...

    public class DependencySetupFixture
    {
        public DependencySetupFixture()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", false, true);

            var configuration = configurationBuilder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("UserConnectionString")));
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebAPIConnectionString")));
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IBlogPostRepository, BlogPostRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IImageRepository, ImageRepository>();
            serviceCollection.AddScoped<ITokenRepository, TokenRepository>(provider =>
            {
                var tokenRepository = new TokenRepository(provider.GetService<Microsoft.Extensions.Configuration.IConfiguration>());
                return tokenRepository;
            });

            //Add EmailService
            EmailConfiguration emailConfiguration = new(
                "dem.iaria@gmail.com",
                "smtp.gmail.com",
                465,
                "dem.iaria@gmail.com",
                "lpmc vmea macn rdbh"
            );

            serviceCollection.AddSingleton<EmailConfiguration>(emailConfiguration);
            serviceCollection.AddScoped<IEmailService, EmailService>(provider =>
            {
                var emailService = new EmailService(provider.GetService<EmailConfiguration>());
                return emailService;
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceCollection.AddDataProtection();
            serviceCollection.AddIdentityCore<IdentityUser>()
                                .AddRoles<IdentityRole>()
                                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("BlogAPP")
                                .AddEntityFrameworkStores<AuthDbContext>()
                                .AddDefaultTokenProviders();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}