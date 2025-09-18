using FluentAssertions;

namespace R3sult4Net.Test;

public class ErrorTest
{
    [Fact]
    public void Error_Test_Simple()
    {
        Error error = new Error("code", "description");
        error.Code.Should().Be("code");
        error.Domain.Should().BeNull();
        error.Description.Should().Be("description");
        error.InnerErrors.Should().NotBeNull();
        error.InnerErrors.Should().HaveCount(0);
    }
    
    [Fact]
    public void Error_Test_InnerErrors()
    {
        Error error = new Error("code", "description", domain: "domain",
        [
            new Error("code_1", "description_1"),
            new Error("code_2", "description_2")
        ]);
        error.Code.Should().Be("code");
        error.Description.Should().Be("description");
        error.Domain.Should().Be("domain");
        error.InnerErrors.Should().NotBeNull();
        error.InnerErrors.Should().HaveCount(2);
        
        error.InnerErrors[0].Code.Should().Be("code_1");
        error.InnerErrors[1].Code.Should().Be("code_2");
        error.InnerErrors[0].Description.Should().Be("description_1");
        error.InnerErrors[1].Description.Should().Be("description_2");
    }
    
}