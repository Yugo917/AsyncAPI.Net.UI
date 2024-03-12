using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AsyncApi.Net.Ui.Models.Attirbuts;

namespace AsyncApi.Net.Ui.Core;

public interface IAsyncApiScanner
{
    public Dictionary<Type, List<MethodInfo>> GetAsyncApiClasses(Assembly assembly);
}

public class AsyncApiScanner : IAsyncApiScanner
{
    public Dictionary<Type, List<MethodInfo>> GetAsyncApiClasses(Assembly assembly)
    {
        var classesWithAsyncApiAttribut = GetAsyncApiServiceTypes(assembly);
        var methodByClasses = new Dictionary<Type, List<MethodInfo>>();

        foreach(var type in classesWithAsyncApiAttribut)
        {
            var methods = GetAsyncApiServiceMethods(type);
            if (methods.Any())
            {
                methodByClasses.Add(type, methods);
            }
        }

        return methodByClasses;
    }

    private List<Type> GetAsyncApiServiceTypes(Assembly assembly)
    {
        return assembly.GetTypes().Where(w => w.GetCustomAttributes(typeof(AsyncApiServiceAttribute), true).Length > 0).ToList();
    }

    private List<MethodInfo> GetAsyncApiServiceMethods(Type type)
    {
        return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                   .Where(w =>
                       w.GetCustomAttributes<AsyncPublishAttribute>().Any() ||
                       w.GetCustomAttributes<AsyncSubscribeAttribute>().Any())
                   .ToList();
    }
}
