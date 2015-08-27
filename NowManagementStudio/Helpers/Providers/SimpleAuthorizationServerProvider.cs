using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using NowManagementStudio.Models;
using System;

namespace NowManagementStudio.Helpers.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Responsible for validating the "Client".  In our case there is only one client
        /// so we will alwasy return validated successfully
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Validates the username and password sent to the authorization server's token endpoint.  We will 
        /// use "AuthRepository" class and call "FindUser" to check if the user name and password are valid.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                var roles = _repo.GetUserRoles(user);

                // If valid - create a "ClaimsIdentity" and pass authentication type to it.
                // In our case "bearer token"
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));



                // rolesForUser now has a list role classes.

                AuthenticationProperties properties = CreateProperties(user.UserName, Newtonsoft.Json.JsonConvert.SerializeObject(roles));

                var ticket = new AuthenticationTicket(identity, properties);
                context.Validated(ticket);
            }



        }


        /// <summary>
        /// Override Token Endpoint to allow for custom properties to be added.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Method for custom properties to be appeneded to the Bearer Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public static AuthenticationProperties CreateProperties(string userName, string Roles)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
        {
            { "userName", userName },
            {"roles",Roles}
        };
            return new AuthenticationProperties(data);
        }


    }
}