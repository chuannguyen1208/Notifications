﻿using MassTransit;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Tools.Routing;
public static class EndpointsBootstrapper
{
    public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
    {
        UseEndpoints(app, typeof(TMarker).Assembly);
    }

    public static void UseEndpoints(this IApplicationBuilder app, Assembly assembly)
    {
        if (assembly is null)
        {
            throw new InvalidOperationException("Passed Assembly is null");
        }
        var endpointTypes = GetEndpointDefinitionsFromAssembly(assembly);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpointsDefinition.ConfigureEndpoints))!
                .Invoke(null, new object[]
                {
                    app
                });
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointDefinitionsFromAssembly(Assembly assembly)
    {
        var endpointDefinitions =
        assembly
        .DefinedTypes
                .Where(x => x is { IsAbstract: false, IsInterface: false } && typeof(IEndpointsDefinition).IsAssignableFrom(x));
        return endpointDefinitions;
    }
}
