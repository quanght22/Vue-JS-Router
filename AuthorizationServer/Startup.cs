using AuthorizationServer.Models;
using CryptoHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Core;
using OpenIddict.Models;
using System.Linq;

namespace AuthorizationServer {
    public class Startup {
        public IConfigurationRoot Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            services.AddCors();
            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options => {
                // Configure the context to use an in-memory store.
                options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"]);

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // Register the Identity services.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Register the OpenIddict services.
            services.AddOpenIddict()
                // Register the Entity Framework stores.
                .AddEntityFrameworkCoreStores<ApplicationDbContext>()

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                .AddMvcBinders()

                // Enable the token endpoint.
                .EnableTokenEndpoint("/connect/token")

                // Enable the password and the refresh token flows.
                .AllowPasswordFlow()
                .AllowRefreshTokenFlow()

                // During development, you can disable the HTTPS requirement.
                .DisableHttpsRequirement()

                // Register a new ephemeral key, that is discarded when the application
                // shuts down. Tokens signed using this key are automatically invalidated.
                // This method should only be used during development.
                .AddEphemeralSigningKey();
        }

        public void Configure(IApplicationBuilder app) {
            app.UseDeveloperExceptionPage();

            app.UseCors(builder => {
                builder.WithOrigins("http://localhost:5055");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseIdentity();

            app.UseOpenIddict();

            app.UseOAuthValidation();

            app.UseMvcWithDefaultRoute();
            using (var context = new ApplicationDbContext(
                app.ApplicationServices.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                var applications = context.Set<OpenIddictApplication>();

                if (!applications.Any())
                {
                    applications.Add(new OpenIddictApplication
                    {
                        ClientId = "AB08936C-5049-4499-9958-1B8E6E218743",
                        ClientSecret = Crypto.HashPassword("388D45FA-B36B-4988-BA59-B187D329C207"),
                        DisplayName = "My client application",
                        Type = OpenIddictConstants.ClientTypes.Confidential
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
