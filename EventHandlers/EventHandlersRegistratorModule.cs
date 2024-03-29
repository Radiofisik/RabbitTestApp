﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;

namespace EventHandlers
{
    public class EventHandlersRegistratorModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types =
                GetType().Assembly.GetTypes()
                    .Where(type => typeof(IHandleMessages).IsAssignableFrom(type))
                    .ToArray();

            builder.RegisterTypes(types)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterRebus((configurer, context) => configurer
                    .Logging(l => l.Serilog())
                    .Transport(t => t.UseRabbitMq("amqp://docker", "testappqueue"))
                    .Timeouts(t => t.StoreInMemory())
                .Options(o => {
                    o.SetNumberOfWorkers(1);
                    o.SetMaxParallelism(30);
                        
                }));

            builder.RegisterType<EventSubscriber>().AsImplementedInterfaces();

        }
    }
}
