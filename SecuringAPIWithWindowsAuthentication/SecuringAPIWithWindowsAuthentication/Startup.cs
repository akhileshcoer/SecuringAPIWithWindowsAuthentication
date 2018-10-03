using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAPIWithWindowsAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddAuthorization(options =>
            {
                options.InvokeHandlersAfterFailure = true;
                options.AddPolicy("Administrator", policy =>
                {
                    policy.Requirements.Add(new SinglePolicyRequirement("Administrator"));
                });

                options.AddPolicy("ALI\\ATIN-FC-Systems-Dev", policy =>
                {
                    policy.Requirements.Add(new SinglePolicyRequirement("ALI\\ATIN-FC-Systems-Dev"));
                });

            });
            services.AddSingleton<IAuthorizationHandler, SinglePolicyHandler>();
            services.AddSingleton<IAuthorizationHandler, MultipleOrPolicyHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(builder =>
            builder
            .WithOrigins("http://localhost:4200")
            .WithHeaders("Origin, Content-Type, X-Auth-Token")
            .WithMethods("GET, POST, PATCH, PUT, DELETE, OPTIONS").AllowCredentials());

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
