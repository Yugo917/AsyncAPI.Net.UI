using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AsyncApi.Net.Ui.Models.Attirbuts;

namespace AsyncApi.Net.Ui.Core;

public class GroupedClassesByVersionProtocol
{
    public GroupByVersionProtocol Group { get; set; }

    public Dictionary<Type, List<MethodInfo>> Classes { get; set; }
}

public class GroupByVersionProtocol
{
    public string Version { get; set; }

    public string Protocol { get; set; }

    public string GroupTitle
    {
        get => $"{Protocol} _ {Version}";
    }
}

public interface IAsyncApiServiceGrouper
{
    List<GroupedClassesByVersionProtocol> ToGroupedClasses(Dictionary<Type, List<MethodInfo>> classes);
}

public class AsyncApiServiceGrouper : IAsyncApiServiceGrouper
{
    public List<GroupedClassesByVersionProtocol> ToGroupedClasses(Dictionary<Type, List<MethodInfo>> classes)
    {
        var classesGroupedByVersionAnProtocol = new List<GroupedClassesByVersionProtocol>();

        // create list of unique key (Version + ServerProtocol)
        var keyGroups = classes.GroupBy(g => new
        {
            GetTypedAttribut<AsyncApiServiceAttribute>(g.Key).Version, GetTypedAttribut<AsyncApiServiceAttribute>(g.Key).ServerProtocol
        }).ToList();

        // for each unique key (Version + ServerProtocol) add binded classes
        foreach (var group in keyGroups)
        {
            var classesByVersionAnProtocol = new GroupedClassesByVersionProtocol()
            {
                Group = new GroupByVersionProtocol() { Version = group.Key.Version, Protocol = group.Key.ServerProtocol },
                Classes = new Dictionary<Type, List<MethodInfo>>()
            };
            foreach (var classe in classes)
            {
                var service = GetTypedAttribut<AsyncApiServiceAttribute>(classe.Key);
                if (group.Key.Version == service.Version && group.Key.ServerProtocol == service.ServerProtocol)
                {
                    classesByVersionAnProtocol.Classes.Add(classe.Key, classe.Value);
                }
            }

            classesGroupedByVersionAnProtocol.Add(classesByVersionAnProtocol);
        }

        return classesGroupedByVersionAnProtocol;
    }

    private TAttribut GetTypedAttribut<TAttribut>(Type t)
    {
        var attribute = (TAttribut)(object)Attribute.GetCustomAttribute(t, typeof(TAttribut));
        return attribute;
    }
}
