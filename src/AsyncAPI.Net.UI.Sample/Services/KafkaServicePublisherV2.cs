using AsyncApi.Net.Ui.Models.Attirbuts;
using AsyncApi.Net.Ui.Sample.Models;

// ReSharper disable UnusedParameter.Local
namespace AsyncApi.Net.Ui.Sample.Services;

[AsyncApiService(Version = "2.0.0", ServerProtocol = "kafka")]
public class KafkaServicePublisherV2
{
    public void PublishEvent2(SimpleMessageV2 message)
    {
        PrivateSubMethodPublishEvent2(message);
    }

    /// <summary>
    /// PublishEvent2.
    /// (with message without header and payload property)
    /// (without explicit async publish info on the method attribut).
    /// </summary>
    /// <param name="message"></param>
    [AsyncPublish(ChannelName = "KafkaServicePublisherV2_SimpleMessageV2", MessageType = typeof(SimpleMessageV2))]
    private void PrivateSubMethodPublishEvent2(SimpleMessageV2 message)
    {
        // PublishEvent2 ..........................
    }
}
