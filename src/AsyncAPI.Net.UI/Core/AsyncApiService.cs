using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using AsyncApi.Net.Ui.Helper;
using LEGO.AsyncAPI.Models;
using LEGO.AsyncAPI.Writers;

namespace AsyncApi.Net.Ui.Core;

public interface IAsyncApiService
{
    Dictionary<string, string> GenerateJsonAsyncApiDocs(Assembly assembly);
}

public class AsyncApiService : IAsyncApiService
{
    private readonly IAsyncApiScanner asyncApiScanner;
    private readonly IAsyncApiDocumentBuilder asyncApiDocumentBuilder;
    private readonly IAsyncApiServiceGrouper asyncApiServiceGrouper;
    private Dictionary<string, string> cachedJsonAsyncApiDocs;

    public AsyncApiService(IAsyncApiScanner asyncApiScanner, IAsyncApiDocumentBuilder asyncApiDocumentBuilder, IAsyncApiServiceGrouper asyncApiServiceGrouper)
    {
        this.asyncApiScanner = asyncApiScanner;
        this.asyncApiDocumentBuilder = asyncApiDocumentBuilder;
        this.asyncApiServiceGrouper = asyncApiServiceGrouper;
    }

    /// <summary>
    /// Generate a dictionary.  [AsyncApiDocument.Title, AsyncApiDocumentJson] of all AsyncApiService present in the assembly.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GenerateJsonAsyncApiDocs(Assembly assembly)
    {
        if (cachedJsonAsyncApiDocs == null)
        {
            cachedJsonAsyncApiDocs = new Dictionary<string, string>();
            var classes = asyncApiScanner.GetAsyncApiClasses(assembly);
            var groupedClasses = asyncApiServiceGrouper.ToGroupedClasses(classes);
            foreach (var item in groupedClasses)
            {
                var asyncDoc = asyncApiDocumentBuilder.CreateAsyncApiDocument(item.Group.Version, item.Group.Protocol, item.Classes);
                var asyncDocJson = AsyncApiDocumentToJson(asyncDoc);
                cachedJsonAsyncApiDocs.Add(item.Group.GroupTitle, asyncDocJson);
            }
        }

        return cachedJsonAsyncApiDocs;
    }

    private string AsyncApiDocumentToJson(AsyncApiDocument asyncApiDocument)
    {
        var outputString = new StringWriter(CultureInfo.InvariantCulture);
        var writer = new AsyncApiJsonWriter(outputString, new AsyncApiWriterSettings { InlineReferences = false });
        asyncApiDocument.SerializeV2(writer);
        var asyncApiDocumentJsonString = outputString.GetStringBuilder().ToString();
        var minified = JsonHelper.Minify(asyncApiDocumentJsonString);
        return minified;
    }
}
