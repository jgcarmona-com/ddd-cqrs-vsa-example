namespace Jgcarmona.Qna.Services.NotificationService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies();

            // Search for all types that implement IEventHandler<T>
            var handlerTypes = assembliesToScan
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .ToList();

            foreach (var handlerType in handlerTypes)
            {
                // Get the interface type
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));
                // Register the handler
                services.AddScoped(interfaceType, handlerType);
            }

            return services;
        }
    }
}
