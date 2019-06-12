using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Events;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace EventHandlers
{
    class EventSubscriber: IStartable
    {
        private readonly IBus _bus;
        private readonly ILogger<EventSubscriber> _logger;

        public EventSubscriber(IBus bus, ILogger<EventSubscriber> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation("subscribe to TestEvent");
            _bus.Subscribe<TestEvent>().Wait();

            //for test only
            _logger.LogInformation("publish TestEvent");
            _bus.Publish(new TestEvent());
        }
    }
}
