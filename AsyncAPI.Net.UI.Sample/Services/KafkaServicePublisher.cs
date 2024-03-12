using AsyncApi.Net.Ui.Models.Attirbuts;
using AsyncApi.Net.Ui.Sample.Models;

namespace AsyncApi.Net.Ui.Sample.Services;

[AsyncApiService(Version = "1.0.0", ServerProtocol = "kafka")]
public class KafkaServicePublisher
{
    /// <summary>
    /// PublishEvent1.
    /// (with message with header and payload property)
    /// (with explicit async publish info on the method attribut).
    /// </summary>
    /// <param name="message"></param>
    [AsyncPublish(ChannelName = "KafkaServicePublisherV1_Message", OperationDescription = "publish event", MessageName = nameof(Message), MessageType = typeof(Message))]
    public void PublishEvent(Message message)
    {
        // PublishEvent ..........................
    }

    /// <summary>
    /// PublishEvent2.
    /// (with message without header and payload property)
    /// (without explicit async publish info on the method attribut).
    /// </summary>
    /// <param name="message"></param>
    [AsyncPublish(ChannelName = "KafkaServicePublisherV1_SimpleMessage", MessageType = typeof(SimpleMessage))]
    public void PublishEvent2(SimpleMessage message)
    {
        // PublishEvent2 ..........................
    }
}
