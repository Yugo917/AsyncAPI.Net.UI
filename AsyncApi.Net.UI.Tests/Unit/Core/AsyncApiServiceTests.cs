using System;
using System.Collections.Generic;
using System.Linq;
using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Models;
using FluentAssertions;
using Xunit;

namespace AsyncApi.Net.UI.Tests.Unit.Core;

[Trait("Category", "Unit")]
public class AsyncApiServiceTests
{
    [Fact]
    public void GenerateJsonAsyncApiDocs_ShouldSucceed()
    {
        // Arrange
        var sampleAssembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName?.Contains("AsyncAPI.Net.UI.Sample") ?? false);
        var asyncApiService = new AsyncApiService(
            new AsyncApiScanner(),
            new AsyncApiDocumentBuilder(
                new AsyncApiAttributeConverter(),
                new AsyncApiDocInfo()
                {
                    Title = "My title AsyncAPI",
                    Description = "The great description of the my AcyncApi",
                    ContactName = "Crazy Coder",
                    ContactUrl = "https://www.microsoft.com/"
                }),
            new AsyncApiServiceGrouper());
        var expectedResult = new Dictionary<string, string>();
        expectedResult.Add("kafka _ 1.0.0", "{\"asyncapi\":\"2.6.0\",\"info\":{\"title\":\"My title AsyncAPI\",\"version\":\"1.0.0\",\"description\":\"The great description of the my AcyncApi\",\"contact\":{\"name\":\"Crazy Coder\",\"url\":\"https://www.microsoft.com/\"}},\"servers\":{\"server\":{\"url\":\"\",\"protocol\":\"kafka\"}},\"channels\":{\"KafkaServicePublisherV1_Message\":{\"description\":\"KafkaServicePublisherV1_Message\",\"publish\":{\"operationId\":\"KafkaServicePublisherV1_Message\",\"description\":\"publish event\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"name\":\"Message\",\"headers\":{\"type\":\"object\",\"properties\":{\"eventType\":{\"description\":\"Type of action performed on the entity.\",\"type\":\"string\",\"enum\":[\"Create\",\"Update\",\"Upsert\",\"Delete\"]},\"eventTime\":{\"description\":\"Time (in UTC) when the change happened on the source entity.\",\"type\":\"string\",\"format\":\"date-time\"},\"eventId\":{\"description\":\"Unique identifier for the message being sent.\",\"type\":\"string\"},\"schemaName\":{\"description\":\"Name of the schema of the payload + the version.\",\"type\":\"string\"},\"actorName\":{\"description\":\"Name of the actor who produced the message + the version number.\",\"type\":\"string\"}}},\"payload\":{\"type\":\"object\",\"properties\":{\"propA1\":{\"description\":\"PropA1 description.\",\"type\":\"string\"},\"propWithoutDescriptionA2\":{\"type\":\"integer\"},\"propA3\":{\"description\":\"PropA2 description.\",\"type\":\"integer\"}}}}}},\"KafkaServicePublisherV1_SimpleMessage\":{\"description\":\"KafkaServicePublisherV1_SimpleMessage\",\"publish\":{\"operationId\":\"KafkaServicePublisherV1_SimpleMessage\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"payload\":{\"type\":\"object\",\"properties\":{\"prop1\":{\"type\":\"string\"}}}}}},\"KafkaServiceSubscriberV1_Message\":{\"description\":\"KafkaServiceSubscriberV1_Message\",\"subscribe\":{\"operationId\":\"KafkaServiceSubscriberV1_Message\",\"description\":\"handle event\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"name\":\"Message\",\"headers\":{\"type\":\"object\",\"properties\":{\"eventType\":{\"description\":\"Type of action performed on the entity.\",\"type\":\"string\",\"enum\":[\"Create\",\"Update\",\"Upsert\",\"Delete\"]},\"eventTime\":{\"description\":\"Time (in UTC) when the change happened on the source entity.\",\"type\":\"string\",\"format\":\"date-time\"},\"eventId\":{\"description\":\"Unique identifier for the message being sent.\",\"type\":\"string\"},\"schemaName\":{\"description\":\"Name of the schema of the payload + the version.\",\"type\":\"string\"},\"actorName\":{\"description\":\"Name of the actor who produced the message + the version number.\",\"type\":\"string\"}}},\"payload\":{\"type\":\"object\",\"properties\":{\"propA1\":{\"description\":\"PropA1 description.\",\"type\":\"string\"},\"propWithoutDescriptionA2\":{\"type\":\"integer\"},\"propA3\":{\"description\":\"PropA2 description.\",\"type\":\"integer\"}}}}}},\"KafkaServiceSubscriberV1_KafkaChannel_SimpleMessage\":{\"description\":\"KafkaServiceSubscriberV1_KafkaChannel_SimpleMessage\",\"subscribe\":{\"operationId\":\"KafkaServiceSubscriberV1_KafkaChannel_SimpleMessage\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"payload\":{\"type\":\"object\",\"properties\":{\"prop1\":{\"type\":\"string\"}}}}}}},\"defaultContentType\":\"application/json\"}");
        expectedResult.Add("kafka _ 2.0.0", "{\"asyncapi\":\"2.6.0\",\"info\":{\"title\":\"My title AsyncAPI\",\"version\":\"2.0.0\",\"description\":\"The great description of the my AcyncApi\",\"contact\":{\"name\":\"Crazy Coder\",\"url\":\"https://www.microsoft.com/\"}},\"servers\":{\"server\":{\"url\":\"\",\"protocol\":\"kafka\"}},\"channels\":{\"KafkaServicePublisherV2_SimpleMessageV2\":{\"description\":\"KafkaServicePublisherV2_SimpleMessageV2\",\"publish\":{\"operationId\":\"KafkaServicePublisherV2_SimpleMessageV2\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"payload\":{\"type\":\"object\",\"properties\":{\"prop1\":{\"type\":\"string\"}}}}}}},\"defaultContentType\":\"application/json\"}");
        expectedResult.Add("rabbitmq _ 1.0.0", "{\"asyncapi\":\"2.6.0\",\"info\":{\"title\":\"My title AsyncAPI\",\"version\":\"1.0.0\",\"description\":\"The great description of the my AcyncApi\",\"contact\":{\"name\":\"Crazy Coder\",\"url\":\"https://www.microsoft.com/\"}},\"servers\":{\"server\":{\"url\":\"\",\"protocol\":\"rabbitmq\"}},\"channels\":{\"RabbitMqServiceSubscriberV1_Message\":{\"description\":\"RabbitMqServiceSubscriberV1_Message\",\"subscribe\":{\"operationId\":\"RabbitMqServiceSubscriberV1_Message\",\"description\":\"handle event\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"name\":\"Message\",\"headers\":{\"type\":\"object\",\"properties\":{\"eventType\":{\"description\":\"Type of action performed on the entity.\",\"type\":\"string\",\"enum\":[\"Create\",\"Update\",\"Upsert\",\"Delete\"]},\"eventTime\":{\"description\":\"Time (in UTC) when the change happened on the source entity.\",\"type\":\"string\",\"format\":\"date-time\"},\"eventId\":{\"description\":\"Unique identifier for the message being sent.\",\"type\":\"string\"},\"schemaName\":{\"description\":\"Name of the schema of the payload + the version.\",\"type\":\"string\"},\"actorName\":{\"description\":\"Name of the actor who produced the message + the version number.\",\"type\":\"string\"}}},\"payload\":{\"type\":\"object\",\"properties\":{\"propA1\":{\"description\":\"PropA1 description.\",\"type\":\"string\"},\"propWithoutDescriptionA2\":{\"type\":\"integer\"},\"propA3\":{\"description\":\"PropA2 description.\",\"type\":\"integer\"}}}}}},\"RabbitMqServiceSubscriberV1_SimpleMessage\":{\"description\":\"RabbitMqServiceSubscriberV1_SimpleMessage\",\"subscribe\":{\"operationId\":\"RabbitMqServiceSubscriberV1_SimpleMessage\",\"message\":{\"schemaFormat\":\"application/vnd.aai.asyncapi;version=2.5.0\",\"payload\":{\"type\":\"object\",\"properties\":{\"prop1\":{\"type\":\"string\"}}}}}}},\"defaultContentType\":\"application/json\"}");

        // Act
        var res = asyncApiService.GenerateJsonAsyncApiDocs(sampleAssembly);

        // Assert
        res.Should().BeEquivalentTo(expectedResult);
    }
}
