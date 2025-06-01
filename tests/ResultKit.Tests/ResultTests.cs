using ResultKit;

public class ResultTests
{
    [Fact]
    public void Success_Should_Set_IsSuccess_True()
    {
        var result = Result.Success();
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
        Assert.Null(result.ValidationErrors);
    }

    [Fact]
    public void Failure_Should_Set_IsSuccess_False_And_Error()
    {
        var error = new Error("SAMPLE_CODE", "error message");
        var result = Result.Failure(error);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void ValidationFailure_Should_Set_ValidationErrors()
    {
        var validationErrors = new List<ValidationError>
        {
            new ValidationError("FieldA", "Required")
        };
        var result = Result.ValidationFailure(validationErrors);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Error);
        Assert.NotNull(result.ValidationErrors);
        Assert.Single(result.ValidationErrors!);
    }

    [Fact]
    public void FromException_Should_Wrap_Exception()
    {
        var ex = new InvalidOperationException("deneme");
        var result = Result.FromException(ex);
        Assert.False(result.IsSuccess);
        Assert.Equal("InvalidOperationException", result.Error?.Code);
        Assert.Contains("deneme", result.Error?.Message);
    }
}
