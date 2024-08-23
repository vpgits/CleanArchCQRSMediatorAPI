// <copyright file="MinimalApiExtensions.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Extensions
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;

    public static class MinimalApiExtensions
    {
        public static void MapEndpoint(this WebApplication app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var classes = assemblies.Distinct().SelectMany(a => a.GetTypes()).Where(a => typeof(IApiModule).IsAssignableFrom(a) && !a.IsInterface && !a.IsAbstract);
            foreach (var assembly in classes)
            {
                var instance = Activator.CreateInstance(assembly) as IApiModule;
                instance?.MapEndpoint(app);
            }
        }
    }
}