using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Web_Project.Models;

namespace Web_Project.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token = GetToken(actionContext);

            try {
                if (token == null) throw new Exception();
                string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] creds = decoded.Split(':');
                string username = creds[0];
                string userToken = creds[1];
                using (APIEntities api = new APIEntities())
                {
                    User user = api.Users.Where(u => u.username.Equals(username)).FirstOrDefault();
                    if (user == null) throw new Exception();
                    if (!user.token.Equals(userToken)) throw new Exception();

                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.username), null);
                }
            }
            catch (Exception e)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            base.OnAuthorization(actionContext);
        }

        private string GetToken(HttpActionContext actionContext)
        {
            string token = null;
            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && 
                !String.IsNullOrEmpty(authRequest.Parameter) && 
                authRequest.Scheme == "Basic")
            {
                token = authRequest.Parameter;
            }

            return token;
        }
    }
}