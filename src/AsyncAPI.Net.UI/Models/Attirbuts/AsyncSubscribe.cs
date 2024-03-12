using System;

namespace AsyncApi.Net.Ui.Models.Attirbuts;

/// <summary>
/// Attibute used to identify channel, subscribe operation and message.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AsyncSubscribeAttribute : Attribute, IAsyncApiMethodAttribute
{
    public string ChannelName { get; set; }

    public string OperationDescription { get; set; }

    public OperationType OperationType { get => OperationType.Subscribe; }

    public string MessageName { get; set; }

    public Type MessageType { get; set; }
}
