// <copyright file="MinimalApiExtensions.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Extensions
{
    using CleanArchCQRSMediatorAPI.API.Interfaces;
    using System.Reflection;


    public static class MinimalApiExtensions
    {
        public static void MapEndpoint(this WebApplication app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var classes = assemblies.Distinct().SelectMany(a => a.GetLoadableTypes()).Where(a => typeof(IApiModule).IsAssignableFrom(a) && !a.IsInterface && !a.IsAbstract);
            foreach (var assembly in classes)
            {
                var instance = Activator.CreateInstance(assembly) as IApiModule;
                instance?.MapEndpoint(app);
            }
        }
        // https://github.com/dotnet/SqlClient/issues/1930 refer
        public static Type[] GetLoadableTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t is not null).ToArray()!;
            }
        }
    }
}