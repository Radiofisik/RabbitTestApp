using System;
using System.Threading.Tasks;
using Events;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace EventHandlers
{
    public class TestEventHandler: IHandleMessages<TestEvent>
    {
        private readonly ILogger<TestEventHandler> _logger;
        private readonly IBus _bus;

        public TestEventHandler(ILogger<TestEventHandler> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Handle(TestEvent message)
        {
            _logger.LogInformation("received message");

            //resend to myself again
            await _bus.DeferLocal(TimeSpan.FromSeconds(30), message);
        }
    }
}
