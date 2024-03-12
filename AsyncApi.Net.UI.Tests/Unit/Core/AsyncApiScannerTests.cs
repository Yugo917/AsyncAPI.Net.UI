using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Sample.Services;
using FluentAssertions;
using Xunit;

namespace AsyncApi.Net.UI.Tests.Unit.Core;

[Trait("Category", "Unit")]
public class AsyncApiScannerTests
{
    private static Assembly sampleAssembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName?.Contains("AsyncAPI.Net.UI.Sample") ?? false);

    [Fact]
    public void GetAsyncApiClasses_ShouldSucceed()
    {
        // Arrange
        var asyncApiScanner = new AsyncApiScanner();
        var expectedTypes = new List<Type>()
        {
            typeof(KafkaServicePublisher),
            typeof(KafkaServicePublisherV2),
            typeof(KafkaServiceSubscriber),
            typeof(RabbitMqServiceSubscriber)
        };
        var expectedMethods = new List<string>() { "PublishEvent", "PublishEvent2", "PrivateSubMethodPublishEvent2", "SubscribeEvent", "SubscribeEvent2", "SubscribeEvent", "SubscribeEvent2" };

        // Act
        var res = asyncApiScanner.GetAsyncApiClasses(sampleAssembly);

        // Assert
        res.Select(s => s.Key).SequenceEqual(expectedTypes).Should().BeTrue();
        res.SelectMany(s => s.Value, (_, v) => v.Name).SequenceEqual(expectedMethods).Should().BeTrue();
    }
}
