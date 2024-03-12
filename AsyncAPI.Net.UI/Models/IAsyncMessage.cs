namespace AsyncApi.Net.Ui.Models;

public interface IAsyncMessage<THeader, TPayload>
{
    public THeader Headers { get; set; }

    public TPayload Payload { get; set; }
}
