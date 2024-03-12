using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace AsyncApi.Net.Ui.Core;

public static class AsyncApiGenerator
{
    /// <summary>
    /// Generate the JSON schema
    /// To be use to generate the global asyncapi.json message model infos.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static JSchema GenerateJSchema(Type type)
    {
        var generator = new JSchemaGenerator
        {
            DefaultRequired = Required.DisallowNull,
            SchemaLocationHandling = SchemaLocationHandling.Inline
        };
        generator.GenerationProviders.Add(new StringEnumGenerationProvider());
        generator.ContractResolver = new CamelCasePropertyNamesContractResolver();
        var schema = generator.Generate(type);
        return schema;
    }
}
