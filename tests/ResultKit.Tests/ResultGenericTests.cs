using ResultKit;

public class ResultGenericTests
{
    [Fact]
    public void Success_Should_Set_Value_And_IsSuccess_True()
    {
        var result = Result<string>.Success("abc");
        Assert.True(result.IsSuccess);
        Assert.Equal("abc", result.Value);
    }

    [Fact]
    public void Failure_Should_Set_Error()
    {
        var error = new Error("CODE", "err");
        var result = Result<string>.Failure(error);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void ValidationFailure_Should_Set_ValidationErrors()
    {
        var list = new List<ValidationError> { new ValidationError("f", "msg") };
        var result = Result<string>.ValidationFailure(list);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.ValidationErrors);
        Assert.Single(result.ValidationErrors!);
    }

    [Fact]
    public void FromException_Should_Wrap_Exception()
    {
        var ex = new Exception("fail");
        var result = Result<string>.FromException(ex);
        Assert.False(result.IsSuccess);
        Assert.Equal("Exception", result.Error?.Code);
        Assert.Contains("fail", result.Error?.Message);
    }

    [Fact]
    public void ImplicitOperator_Should_Return_Success()
    {
        Result<string> result = "hi";
        Assert.True(result.IsSuccess);
        Assert.Equal("hi", result.Value);
    }
}
