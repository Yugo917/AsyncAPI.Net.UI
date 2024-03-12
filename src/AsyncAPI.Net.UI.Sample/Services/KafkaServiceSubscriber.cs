using AsyncApi.Net.Ui.Models.Attirbuts;
using AsyncApi.Net.Ui.Sample.Models;

namespace AsyncApi.Net.Ui.Sample.Services;

[AsyncApiService(Version = "1.0.0", ServerProtocol = "kafka")]
public class KafkaServiceSubscriber
{
    /// <summary>
    /// SubscribeEvent.
    /// (with message with header and payload property)
    /// (with explicit async publish info on the method attribut).
    /// </summary>
    /// <param name="message"></param>
    [AsyncSubscribe(ChannelName = "KafkaServiceSubscriberV1_Message", OperationDescription = "handle event", MessageName = nameof(Message), MessageType = typeof(Message))]
    public void SubscribeEvent(Message message)
    {
        // SubscribeEvent ..........................
    }

    /// <summary>
    /// SubscribeEvent2.
    /// (with message without header and payload property)
    /// (without explicit async publish info on the method attribut).
    /// </summary>
    /// <param name="message"></param>
    [AsyncSubscribe(ChannelName = "KafkaServiceSubscriberV1_KafkaChannel_SimpleMessage", MessageType = typeof(SimpleMessage))]
    public void SubscribeEvent2(SimpleMessage message)
    {
        // SubscribeEvent2 ..........................
    }
}
