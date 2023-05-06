using Microsoft.AspNetCore.Authentication.Cookies;

namespace SimpleERP.MVC.Configurations
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LogoutPath = "/Auth/Login";
                    options.AccessDeniedPath = "/access-denied";
                });
        }

        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
