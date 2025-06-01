using ResultKit;

public class ErrorTests
{
    [Fact]
    public void Constructor_Should_Set_Code_And_Message()
    {
        var error = new Error("TEST_CODE", "Test message");
        Assert.Equal("TEST_CODE", error.Code);
        Assert.Equal("Test message", error.Message);
    }

    [Fact]
    public void ToString_Should_Format_Output()
    {
        var error = new Error("E", "msg");
        Assert.Equal("[E] msg", error.ToString());
    }
}
