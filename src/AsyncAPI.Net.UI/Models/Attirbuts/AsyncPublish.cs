using System;

namespace AsyncApi.Net.Ui.Models.Attirbuts;

/// <summary>
/// Attibute used to identify channel, publish operation and message.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AsyncPublishAttribute : Attribute, IAsyncApiMethodAttribute
{
    public string ChannelName { get; set; }

    public string OperationDescription { get; set; }

    public OperationType OperationType { get => OperationType.Publish; }

    public string MessageName { get; set; }

    public Type MessageType { get; set; }
}
