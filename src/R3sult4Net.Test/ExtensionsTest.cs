using FluentAssertions;

namespace R3sult4Net.Test;

public class ExtensionsTest
{
    [Fact]
    public void Map_T_Success()
    {
        Result<string> result = "string";

        var o1 = result.Map(x => x + "_1");
        
        o1.IsSuccess.Should().Be(result.IsSuccess);
        o1.Value.Should().Be("string_1");
    }
    
    [Fact]
    public void Map_T_Failure()
    {
        Result<string> result = new Error("code", "message");
        var o1 = result.Map(x => x + "_1");
        o1.IsFailure.Should().BeTrue();
        o1.Error!.Code.Should().Be("code");
        o1.Error!.Description.Should().Be("message");
    }
}