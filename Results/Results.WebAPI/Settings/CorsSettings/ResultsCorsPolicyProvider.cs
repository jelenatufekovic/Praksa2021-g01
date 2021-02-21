using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Results.WebAPI.Settings.CorsSettings
{
    public class ResultsCorsPolicyProvider : ICorsPolicyProvider
    {
        private CorsPolicy _policy;
        public ResultsCorsPolicyProvider()
        {
            _policy = new CorsPolicy()
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
            };

            // Default origin for React client (http://localhost:3000)
            _policy.Origins.Add(ConfigurationManager.AppSettings.Get("ResultsReactClient").ToString()); 
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }

    }
}