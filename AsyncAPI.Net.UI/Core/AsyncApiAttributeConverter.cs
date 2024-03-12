using System;
using System.Collections.Generic;
using System.Linq;
using AsyncApi.Net.Ui.Models;
using AsyncApi.Net.Ui.Models.Attirbuts;
using LEGO.AsyncAPI.Models;
using LEGO.AsyncAPI.Models.Any;
using LEGO.AsyncAPI.Models.Interfaces;
using AsyncApiInfo = LEGO.AsyncAPI.Models.AsyncApiInfo;

namespace AsyncApi.Net.Ui.Core;

public interface IAsyncApiAttributeConverter
{
    public AsyncApiDocument ToAsyncApiDocument(AsyncApiDocInfo asyncApiDocInfo, string version, string protocol);

    public (AsyncApiChannel Channel, AsyncApiOperation Operation, AsyncApiMessage Message) ToAsyncApiElement(IAsyncApiMethodAttribute asyncApiMethodAttribute);
}

public class AsyncApiAttributeConverter : IAsyncApiAttributeConverter
{
    public AsyncApiDocument ToAsyncApiDocument(AsyncApiDocInfo asyncApiDocInfo, string version, string protocol)
    {
        var asyncApiDocument = new AsyncApiDocument();
        asyncApiDocument.Extensions = new Dictionary<string, IAsyncApiExtension>()
        {
            { "defaultContentType", new AsyncApiString("application/json") }
        };
        asyncApiDocument.Info = new AsyncApiInfo()
        {
            Title = asyncApiDocInfo.Title,
            Contact = new AsyncApiContact()
            {
                Name = asyncApiDocInfo.ContactName,
                Url = new Uri(asyncApiDocInfo.ContactUrl),

                // Email = String.Empty // required in the spec
            },
            Description = asyncApiDocInfo.Description,
            Version = version
        };
        asyncApiDocument.Servers = new Dictionary<string, AsyncApiServer>
        {
            {
                "server", new AsyncApiServer
                {
                    Url = string.Empty, // required in the spec
                    Protocol = protocol
                }
            },
        };

        return asyncApiDocument;
    }

    public (AsyncApiChannel Channel, AsyncApiOperation Operation, AsyncApiMessage Message) ToAsyncApiElement(IAsyncApiMethodAttribute asyncApiMethodAttribute)
    {
        return (ToAsyncApiChannel(asyncApiMethodAttribute.ChannelName),
            ToAsyncApiOperation(asyncApiMethodAttribute.ChannelName, asyncApiMethodAttribute.OperationDescription),
            ToAsyncApiMessage(asyncApiMethodAttribute.MessageName, asyncApiMethodAttribute.MessageType));
    }

    private AsyncApiChannel ToAsyncApiChannel(string channelDescription)
    {
        var asyncApiElement = new AsyncApiChannel();
        asyncApiElement.Description = channelDescription;
        return asyncApiElement;
    }

    private AsyncApiOperation ToAsyncApiOperation(string operationName, string operationDescription)
    {
        var asyncApiElement = new AsyncApiOperation();
        asyncApiElement.OperationId = operationName;
        asyncApiElement.Description = operationDescription;
        return asyncApiElement;
    }

    private AsyncApiMessage ToAsyncApiMessage(string messageName, Type messageType)
    {
        var asyncApiElement = new AsyncApiMessage();
        asyncApiElement.MessageId = messageName;
        asyncApiElement.Name = messageName;
        asyncApiElement.SchemaFormat = "application/vnd.aai.asyncapi;version=2.5.0";
        if (IsIAsyncMessageWithHeader(messageType))
        {
            var headerType = messageType.GetProperties().First(f => f.Name.ToLower() == "headers").PropertyType;
            var payloadType = messageType.GetProperties().First(f => f.Name.ToLower() == "payload").PropertyType;

            asyncApiElement.Extensions = new Dictionary<string, IAsyncApiExtension>()
            {
                { "headers", new AsyncApiJSchema(headerType) },
                { "payload", new AsyncApiJSchema(payloadType) }
            };
        }
        else
        {
            asyncApiElement.Extensions = new Dictionary<string, IAsyncApiExtension>()
            {
                { "payload", new AsyncApiJSchema(messageType) }
            };
        }

        return asyncApiElement;
    }

    private bool IsIAsyncMessageWithHeader(Type type)
    {
        return type.GetProperties().Select(s => s.Name.ToLower()).Intersect(new List<string>() { "headers", "payload" }).Count() == 2;
    }
}
