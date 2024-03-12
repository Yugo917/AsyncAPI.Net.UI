using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Models;
using AsyncApi.Net.Ui.Models.Attirbuts;
using AsyncApi.Net.Ui.Sample.Models;
using FluentAssertions;
using Xunit;

namespace AsyncApi.Net.UI.Tests.Unit.Core;

[Trait("Category", "Unit")]
public class AsyncApiAttributeConverterTests
{
    private AsyncApiAttributeConverter asyncApiAttributeConverter = new AsyncApiAttributeConverter();

    [Fact]
    public void ToAsyncApiDocument_ShouldSucceed()
    {
        // Arrange
        var asyncApiDocInfo = new AsyncApiDocInfo()
        {
            Title = "Title", Description = "Description", ContactName = "ContactName", ContactUrl = "http://ContactUrl"
        };

        // Act
        var res = asyncApiAttributeConverter.ToAsyncApiDocument(asyncApiDocInfo, "version", "protocol");

        // Assert
        res.Info.Title.Should().Be("Title");
        res.Info.Description.Should().Be("Description");
        res.Info.Version.Should().Be("version");
        res.Info.Contact.Name.Should().Be("ContactName");
        res.Info.Contact.Url.Should().Be("http://ContactUrl");
        res.Servers["server"].Protocol.Should().Be("protocol");
    }

    [Fact]
    public void ToAsyncApiElement_WithSimplePayload_ShouldSucceed()
    {
        // Arrange
        var asyncPublishAttribute = new AsyncPublishAttribute()
        {
            ChannelName = "ChannelName",
            OperationDescription = "OperationDescription",
            MessageName = "SimpleMessage",
            MessageType = typeof(SimpleMessage)
        };

        // Act
        var (channel, operation, message) = asyncApiAttributeConverter.ToAsyncApiElement(asyncPublishAttribute);

        // Assert
        channel.Description.Should().Be("ChannelName");
        operation.OperationId.Should().Be("ChannelName");
        operation.Description.Should().Be("OperationDescription");
        message.MessageId.Should().Be("SimpleMessage");
        message.Name.Should().Be("SimpleMessage");
        message.SchemaFormat.Should().Be("application/vnd.aai.asyncapi;version=2.5.0");
        message.Extensions.Keys.Contains("headers").Should().BeFalse();
        message.Extensions.Keys.Contains("payload").Should().BeTrue();
    }

    [Fact]
    public void ToAsyncApiElement_WithHeaderAndPayload_ShouldSucceed()
    {
        // Arrange
        var asyncPublishAttribute = new AsyncPublishAttribute()
        {
            ChannelName = "ChannelName",
            OperationDescription = "OperationDescription",
            MessageName = "Message",
            MessageType = typeof(Message)
        };

        // Act
        var (channel, operation, message) = asyncApiAttributeConverter.ToAsyncApiElement(asyncPublishAttribute);

        // Assert
        channel.Description.Should().Be("ChannelName");
        operation.OperationId.Should().Be("ChannelName");
        operation.Description.Should().Be("OperationDescription");
        message.MessageId.Should().Be("Message");
        message.Name.Should().Be("Message");
        message.SchemaFormat.Should().Be("application/vnd.aai.asyncapi;version=2.5.0");
        message.Extensions.Keys.Contains("headers").Should().BeTrue();
        message.Extensions.Keys.Contains("payload").Should().BeTrue();
    }
}
