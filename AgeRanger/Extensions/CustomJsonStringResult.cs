using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AgeRanger.Extensions
{
    public class JsonStringResult : IHttpActionResult
    {
        private readonly string _json;
        private readonly HttpStatusCode _statusCode;
        private readonly HttpRequestMessage _request;

        public JsonStringResult(HttpRequestMessage httpRequestMessage, HttpStatusCode statusCode = HttpStatusCode.OK, string json = "")
        {
            _request = httpRequestMessage;
            _json = json;
            _statusCode = statusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var response = _request.CreateResponse(_statusCode);
            response.Content = new StringContent(_json, Encoding.UTF8, "application/json");
            return response;
        }
    }
}