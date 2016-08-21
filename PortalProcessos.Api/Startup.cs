using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using PortalProcessos.Api.PortalContext;
using PortalProcessos.Api.UnitOfWork;
using PortalProcessos.Api.Repository;
using PortalProcessos.Api.Repository.Interface;

[assembly: OwinStartup(typeof(PortalProcessos.Api.Startup))]

namespace PortalProcessos.Api
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public SimpleInjector.Container Container = null;

        public void Configuration(IAppBuilder app)
        {
            
            this.Container = new SimpleInjector.Container();
            InitializeContainer(Container);
            Container.Verify();

            app.Use(async (context, next) =>
            {
                using (Container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            config.DependencyResolver = new SimpleInjector.Integration.WebApi.SimpleInjectorWebApiDependencyResolver(this.Container);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            //Token Consumption
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

        private static void InitializeContainer(Container container)
        {
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            container.Register<PortalContext.PortalContext>(Lifestyle.Scoped);
            container.Register(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), Lifestyle.Scoped);

            container.RegisterWebApiRequest<IPortalRepository, PortalRepository>();
            container.RegisterWebApiRequest<IUserRepository, UserRepository>();
        }

    }
}
