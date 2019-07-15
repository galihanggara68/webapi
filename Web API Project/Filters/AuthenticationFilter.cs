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
using Web_API_Project.Models;

namespace Web_API_Project.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token = GetToken(actionContext);

            if(token != null)
            {
                string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] creds = decoded.Split(':');
                int username = int.Parse(creds[0]);
                string password = creds[1];
                using(HREntities hr = new HREntities())
                {
                    COPY_EMP user = hr.COPY_EMP.Find(username);
                    if (user != null && password.Equals(user.FIRST_NAME))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.FIRST_NAME), null);
                    }
                    else
                    {
                        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
            }
            else
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