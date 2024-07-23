using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebAPP.API.Controllers;
using WebAPP.API.Repositories.Interface;
using Moq;
using WebAPP.API.Models.DTO.RequestDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using WebAPP.API.Data;
using Microsoft.EntityFrameworkCore;
using WebAPP.API.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.DataProtection;

namespace BlogAPP.Test
{
    public class AuthControllerTests : DependencySetupFixture
    {
        [Fact]
        public async Task Register_success()
        {
            //Arrange
            using var scope = ServiceProvider.CreateScope();

            var dataProtectionProvider = scope.ServiceProvider.GetService<IDataProtectionProvider>();
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var tokenRepository = scope.ServiceProvider.GetService<ITokenRepository>();
            var userRepository = scope.ServiceProvider.GetService<IUserRepository>();

            if (userManager == null || tokenRepository == null || userRepository == null || dataProtectionProvider == null)
                throw new ArgumentNullException("Service is null");

            var authController = new AuthController(userManager, tokenRepository, userRepository);

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

            var actionResult = await authController.Register(registerRequestDto);

            //Assert
            Assert.IsType<ViewResult>(actionResult);
            Assert.IsAssignableFrom<RegisterRequestDto>(((ViewResult)actionResult).Model);
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