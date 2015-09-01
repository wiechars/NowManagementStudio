using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using NowManagementStudio.Helpers.Providers;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(NowManagementStudio.API.Startup))]
namespace NowManagementStudio.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        /// <summary>
        /// Create new instance of OAuth Server Options and set the following options:
        /// 1. Path for generating tokens : http://localhost:port/token
        /// 2. Token expires in 24 hours.  ** May need to change
        /// 3. Specified the implementation on how to validate credentials "SimpleAuthorizationServerProvider"
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}