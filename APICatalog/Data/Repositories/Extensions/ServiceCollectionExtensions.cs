using System.Reflection;
using Scrutor;
using Microsoft.Extensions.DependencyModel;

namespace APICatalog.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClassesMatchingInterfaces(
        this IServiceCollection services,
        string @namespace)
    {
        var assemblies = DependencyContext.Default
            .GetDefaultAssemblyNames()
            .Where(assembly => assembly.FullName.StartsWith(@namespace))
            .Select(Assembly.Load);

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}