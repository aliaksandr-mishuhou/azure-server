using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Logging;
using EventGateway.Core;

namespace EventGateway.Server.Api.Controllers
{
    public class MessagesController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger<MessagesController>();

        private readonly IMessageProcessor _messageProcessor;

        public MessagesController(IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        // GET api/messages
        public IEnumerable<Message> Get()
        {
            return new[] {new Message()};
        }

        // POST api/messages
        public async Task<HttpResponseMessage> Post([FromBody]Message message)
        {
            try
            {
                Logger.Trace(m => m($"Processing new message: {message}"));

                await _messageProcessor.Process(message);

                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                Logger.Error(m => m($"Error while processing message: {message}"), ex);

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
