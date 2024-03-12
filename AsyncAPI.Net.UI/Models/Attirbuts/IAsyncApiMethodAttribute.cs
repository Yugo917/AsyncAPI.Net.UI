using System;

namespace AsyncApi.Net.Ui.Models.Attirbuts;

public interface IAsyncApiMethodAttribute
{
    public string ChannelName { get; set; }

    public string OperationDescription { get; set; }

    public OperationType OperationType { get; }

    public string MessageName { get; set; }

    public Type MessageType { get; set; }
}

public enum OperationType
{
    Publish = 1,
    Subscribe = 2,
}
