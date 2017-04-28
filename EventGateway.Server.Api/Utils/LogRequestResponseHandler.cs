using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;

namespace EventGateway.Server.Api.Utils
{
    public class LogRequestResponseHandler : DelegatingHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger<LogRequestResponseHandler>();

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // log request body
            string requestBody = await request.Content.ReadAsStringAsync();

            Logger.Trace(m => m($"REQUEST: {request.Method} {request.RequestUri}"));
            Logger.Trace(m => m("{0}", requestBody));

            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            Logger.Trace(m => m($"RESPONSE: {request.Method} {request.RequestUri}: {result.StatusCode}"));
            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                Logger.Trace(m => m("{0}", responseBody));
            }

            return result;
        }
    }
}