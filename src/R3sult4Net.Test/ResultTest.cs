using FluentAssertions;

namespace R3sult4Net.Test;

public class ResultTest
{
    [Fact]
    public void TestSuccess()
    {
        var result = Result.Success();
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public void TestFailure()
    {
        var result = Result.Failure(new Error("code", "message"));
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("code");
        result.Error.Description.Should().Be("message");
        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public void Test_Implicit()
    {
        Result result = new Error("code", "message");
        
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("code");
        result.Error.Description.Should().Be("message");
    }
    
    [Fact]
    public void TestTSuccess()
    {
        var result = Result<string>.Success("result");
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.Value.Should().Be("result");
    }
    
    [Fact]
    public void TestTFailure()
    {
        var result = Result<string>.Failure(new Error("code", "message"));
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("code");
        result.Error.Description.Should().Be("message");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
    }
    
    [Fact]
    public void Test_T_Implicit()
    {
        Result<string> resultS = "content";
        resultS.IsSuccess.Should().BeTrue();
        resultS.Error.Should().BeNull();
        resultS.Value.Should().Be("content");
        
        
        Result<string> resultF = new Error("code", "message");
        
        resultF.IsFailure.Should().BeTrue();
        resultF.Error.Should().NotBeNull();
        resultF.Error.Code.Should().Be("code");
        resultF.Error.Description.Should().Be("message");
    }
}