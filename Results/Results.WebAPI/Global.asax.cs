using Results.WebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Routing;


namespace Results.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";

            ContainerConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
