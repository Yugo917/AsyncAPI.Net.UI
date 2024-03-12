using AsyncApi.Net.Ui.Models;

namespace AsyncApi.Net.Ui.Sample.Models;

public class Message : IAsyncMessage<MessageHeader, MessagePayload>
{
    public MessageHeader Headers { get; set; }

    public MessagePayload Payload { get; set; }
}
