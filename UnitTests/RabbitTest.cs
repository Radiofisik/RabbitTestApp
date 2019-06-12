using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Xunit;

namespace UnitTests
{
    public class RabbitTest
    {
        [Fact]
        public async Task SendEventTest()
        {
            var bus = GetBus("testqueue");
            await bus.Bus.Publish(new TestEvent());
        }

        BuiltinHandlerActivator GetBus(string queueName, Func<string, Task> handlerMethod = null)
        {
            var activator = new BuiltinHandlerActivator();

            if (handlerMethod != null)
            {
                activator.Handle(handlerMethod);
            }

            Configure.With(activator)
                .Transport(t =>
                {
                    t.UseRabbitMq("amqp://docker", queueName)
                        .AddClientProperties(new Dictionary<string, string>
                        {
                            {"description", "test"}
                        });
                })
                .Start();

            return activator;
        }
    }
}
