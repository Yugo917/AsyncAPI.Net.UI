using AsyncApi.Net.Ui.Helper;
using FluentAssertions;
using Xunit;

namespace AsyncApi.Net.UI.Tests.Unit.Helper;

[Trait("Category", "Unit")]
public class JsonHelperTests
{
    [Fact]
    public void Minify_ShouldSucceed()
    {
        // Arrange
        var json = @"{
                    ""menu"": {
                        ""id"": ""file"",
                        ""value"": ""File"",
                        ""popup"": {
                            ""menuitem"": [
                                {
                                    ""value"": ""New"",
                                    ""onclick"": ""CreateNewDoc()""
                                },
                                {
                                    ""value"": ""Open"",
                                    ""onclick"": ""OpenDoc()""
                                },
                                {
                                    ""value"": ""Close"",
                                    ""onclick"": ""CloseDoc()""
                                }
                            ]
                        }
                    }
                }";

        // Act
        var res = JsonHelper.Minify(json);

        // Assert
        res.Should()
           .Be(
               "{\"menu\":{\"id\":\"file\",\"value\":\"File\",\"popup\":{\"menuitem\":[{\"value\":\"New\",\"onclick\":\"CreateNewDoc()\"},{\"value\":\"Open\",\"onclick\":\"OpenDoc()\"},{\"value\":\"Close\",\"onclick\":\"CloseDoc()\"}]}}}");
    }
}
