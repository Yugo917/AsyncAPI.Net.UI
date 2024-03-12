using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AsyncApi.Net.Ui.Models;
using AsyncApi.Net.Ui.Models.Attirbuts;
using LEGO.AsyncAPI.Models;

namespace AsyncApi.Net.Ui.Core;

public interface IAsyncApiDocumentBuilder
{
    AsyncApiDocument CreateAsyncApiDocument(string version, string protocol, Dictionary<Type, List<MethodInfo>> classes);
}

public class AsyncApiDocumentBuilder : IAsyncApiDocumentBuilder
{
    private readonly IAsyncApiAttributeConverter asyncApiAttributeConverter;
    private readonly AsyncApiDocInfo asyncApiDocInfo;

    public AsyncApiDocumentBuilder(IAsyncApiAttributeConverter asyncApiAttributeConverter, AsyncApiDocInfo asyncApiDocInfo)
    {
        this.asyncApiAttributeConverter = asyncApiAttributeConverter;
        this.asyncApiDocInfo = asyncApiDocInfo;
    }

    public AsyncApiDocument CreateAsyncApiDocument(string version, string protocol, Dictionary<Type, List<MethodInfo>> classes)
    {
        // Document
        var asyncApiDocument = asyncApiAttributeConverter.ToAsyncApiDocument(
            asyncApiDocInfo,
            version,
            protocol);

        // Channels
        asyncApiDocument.Channels = new Dictionary<string, AsyncApiChannel>();
        foreach (var classe in classes)
        {
            foreach (var method in classe.Value)
            {
                var (operationType, methodAttirbut) = GetMethodAttibut(method);
                var (channel, operation, message) = asyncApiAttributeConverter.ToAsyncApiElement(methodAttirbut);
                asyncApiDocument.Channels.Add(methodAttirbut.ChannelName, channel);

                // Operation
                if (operationType == OperationType.Publish)
                {
                    channel.Publish = operation;
                }
                else
                {
                    channel.Subscribe = operation;
                }

                // Message
                operation.Message = new List<AsyncApiMessage>() { message };
            }
        }

        return asyncApiDocument;
    }

    private (OperationType OperationType, IAsyncApiMethodAttribute IAsyncApiMethodAttribute) GetMethodAttibut(MemberInfo method)
    {
        var methodPublishAttribut = method.GetCustomAttributes<AsyncPublishAttribute>().FirstOrDefault();
        if (methodPublishAttribut != null)
        {
            return (OperationType.Publish, methodPublishAttribut);
        }

        var methodSubcribeAttribut = method.GetCustomAttributes<AsyncSubscribeAttribute>().FirstOrDefault();
        return (OperationType.Subscribe, methodSubcribeAttribut);
    }
}
