using Duende.Bff.Yarp;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace JavaScriptClient
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddControllers();

            // add BFF services and server-side session management
            builder.Services.AddBff();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                    options.DefaultSignOutScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.ClientId = "bff";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.Scope.Add("api1");

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            // add CSRF protection and status code handling for API endpoints
            app.UseBff();
            app.UseAuthorization();

            // local API endpoints
            app.MapControllers()
                .RequireAuthorization()
                .AsBffApiEndpoint();

            app.MapBffManagementEndpoints();

            // enable proxying to remote API
            app.MapRemoteBffApiEndpoint("/remote", "https://demo.duendesoftware.com/api/test")
                .RequireAccessToken();

            return app;
        }
    }
}