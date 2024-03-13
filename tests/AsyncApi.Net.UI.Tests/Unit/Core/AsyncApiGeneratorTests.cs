using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Helper;
using FluentAssertions;
using Xunit;

namespace AsyncApi.Net.UI.Tests.Unit.Core;

[Trait("Category", "Unit")]
public class AsyncApiGeneratorTests
{
    [Fact]
    public void GenerateJSchema_ShouldSucceed()
    {
        // Arrange
        var type = typeof(SampleClass);

        // Act
        var res = AsyncApiGenerator.GenerateJSchema(type);

        // Assert
        var minify = JsonHelper.Minify(res.ToString());
        minify.Should().Be("{\"type\":\"object\",\"properties\":{\"prop1\":{\"type\":\"string\"},\"prop2\":{\"description\":\"desciption prop1\",\"type\":\"integer\"},\"prop3\":{\"type\":\"boolean\"},\"prop4\":{\"type\":\"string\",\"enum\":[\"Val1\",\"Val2\"]}},\"required\":[\"prop1\"]}");
    }
}

public class SampleClass
{
    [Required]
    [NotNull]
    public string Prop1 { get; set; }

    [Description("desciption prop1")]
    public int Prop2 { get; set; }

    public bool Prop3 { get; set; }

    public SampleEnum Prop4 { get; set; }
}

public enum SampleEnum
{
    Val1,
    Val2
}
