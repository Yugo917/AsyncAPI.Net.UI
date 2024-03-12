using System;
using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Helper;
using LEGO.AsyncAPI.Models.Interfaces;
using LEGO.AsyncAPI.Writers;
using Newtonsoft.Json.Schema;

namespace AsyncApi.Net.Ui.Models;

/// <summary>
/// AsyncApiJSchema
/// implement IAsyncApiExtension of AsyncApi.DotNet to write the json schema of type.
/// </summary>
public class AsyncApiJSchema : JSchema, IAsyncApiExtension
{
    private readonly Type type;

    public AsyncApiJSchema(Type type)
    {
        this.type = type;
    }

    public void Write(IAsyncApiWriter writer)
    {
        var jsonSchema = AsyncApiGenerator.GenerateJSchema(type).ToString();
        var minified = JsonHelper.Minify(jsonSchema);
        writer.WriteRaw(minified);
    }
}
