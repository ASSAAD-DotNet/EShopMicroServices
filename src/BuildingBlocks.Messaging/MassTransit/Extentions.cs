using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extentions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly? assembly = null)
    {
        //Implement MassTransit RabbitMQ configuration

        return services;
    }
}
