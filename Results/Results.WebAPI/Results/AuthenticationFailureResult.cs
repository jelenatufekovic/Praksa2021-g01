using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Results.WebAPI.Results
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        private readonly string _reasonPhrase;
        private readonly HttpRequestMessage _request;

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            _reasonPhrase = reasonPhrase;
            _request = request;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            response.RequestMessage = _request;
            response.ReasonPhrase = _reasonPhrase;

            return response;
        }
    }
}