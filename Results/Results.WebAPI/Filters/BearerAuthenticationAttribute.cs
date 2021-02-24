using Results.Service;
using Results.Service.Common;
using Results.WebAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Results.WebAPI.Filters
{
    public class BearerAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private string Realm { get; set; } = "ResultsRealm";

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                return;
            }

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing token", request);
                return;
            }

            string token = authorization.Parameter;

            ITokenService tokenService = CreateTokenService();

            IPrincipal principal = await tokenService.GetPrincipalAsync(token);

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
            }

            context.Principal = principal;
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            await Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter;

            if (String.IsNullOrEmpty(Realm))
            {
                parameter = null;
            }
            else
            {
                parameter = "realm=\"" + Realm + "\"";
            }

            context.ChallengeWith("Bearer", parameter);
        }

        private ITokenService CreateTokenService()
        {
            return new TokenService();
        }

        public virtual bool AllowMultiple
        {
            get { return false; }
        }
    }
}