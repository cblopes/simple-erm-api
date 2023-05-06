using SimpleERP.MVC.Extensions;

namespace SimpleERP.MVC.Configurations
{
    public static class WebAppConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<AppSettings>(configuration);

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
                app.UseStatusCodePagesWithRedirects("error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });
            
            return app;
        }
    }
}
