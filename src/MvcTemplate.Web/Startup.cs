﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Routing;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;
using MvcTemplate.Components.Logging;
using MvcTemplate.Components.Mail;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Components.Security;
using MvcTemplate.Controllers;
using MvcTemplate.Data.Core;
using MvcTemplate.Data.Logging;
using MvcTemplate.Data.Migrations;
using MvcTemplate.Services;
using MvcTemplate.Validators;
using System;
using System.IO;

namespace MvcTemplate.Web
{
    public class Startup
    {
        private String ApplicationBasePath { get; }

        public Startup(IApplicationEnvironment env)
        {
            ApplicationBasePath = env.ApplicationBasePath;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterCurrentDependencyResolver(services);
            RegisterLowercaseUrls(services);
            RegisterFilters(services);
            RegisterMvc(services);
        }
        public void Configure(IApplicationBuilder app)
        {
            RegisterAuthorization(app);
            RegisterStaticFiles(app);
            RegisterRoute(app);

            SeedData(app);
        }

        public virtual void RegisterCurrentDependencyResolver(IServiceCollection services)
        {
            services.AddTransient<DbContext, Context>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ILogger, Logger>();
            services.AddTransient<IAuditLogger, AuditLogger>();

            services.AddTransient<IHasher, BCrypter>();
            services.AddTransient<IMailClient>(provider => new SmtpMailClient("smtp.gmail.com", 587, "MVC.Template@gmail.com", "ChangeIt"));

            services.AddTransient<IExceptionFilter, ExceptionFilter>();
            services.AddTransient<IModelMetadataProvider, DisplayNameMetadataProvider>();

            services.AddTransient<IMvcSiteMapParser, MvcSiteMapParser>();
            services.AddSingleton<IMvcSiteMapProvider>(provider => new MvcSiteMapProvider(
                Path.Combine(ApplicationBasePath, "Mvc.sitemap"), provider.GetService<IMvcSiteMapParser>()));

            services.AddSingleton<IGlobalizationProvider>(provider =>
                new GlobalizationProvider(Path.Combine(ApplicationBasePath, "Globalization.xml")));
            services.AddInstance<IAuthorizationProvider>(new AuthorizationProvider(typeof(BaseController).Assembly));

            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IRoleValidator, RoleValidator>();
            services.AddTransient<IAccountValidator, AccountValidator>();
        }
        public virtual void RegisterLowercaseUrls(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }
        public virtual void RegisterFilters(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options => options.Filters.Add(typeof(ExceptionFilter)));
        }
        public virtual void RegisterMvc(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationExpanders.Add(new ViewLocationExpander());
                });
        }

        public virtual void RegisterAuthorization(IApplicationBuilder app)
        {
            Authorization.Provider = new AuthorizationProvider(typeof(BaseController).Assembly);
            Authorization.Provider.Refresh();
        }
        public virtual void RegisterStaticFiles(IApplicationBuilder app)
        {
            app.UseStaticFiles();
        }
        public virtual void RegisterRoute(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "DefaultMultilingualArea",
                    "{language}/{area:exists}/{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index" },
                    new { language = "lt" });

                routes.MapRoute(
                    "DefaultArea",
                    "{area:exists}/{controller}/{action}/{id?}",
                    new { language = "en", controller = "Home", action = "Index" },
                    new { language = "en" });

                routes.MapRoute(
                    "DefaultMultilingual",
                    "{language}/{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index" },
                    new { language = "lt" });

                routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{id?}",
                    new { language = "en", controller = "Home", action = "Index" },
                    new { language = "en" });
            });
        }

        public virtual void SeedData(IApplicationBuilder app)
        {
            using (Configuration configuration = new Configuration(app.ApplicationServices.GetService<DbContext>()))
                configuration.Seed();
        }
    }
}
