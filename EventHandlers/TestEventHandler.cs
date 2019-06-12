using System;
using System.Threading.Tasks;
using Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace EventHandlers
{
    public class TestEventHandler: IHandleMessages<TestEvent>
    {
        private readonly ILogger<TestEventHandler> _logger;

        public TestEventHandler(ILogger<TestEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(TestEvent message)
        {
            _logger.LogInformation("received message");
        }
    }
}
