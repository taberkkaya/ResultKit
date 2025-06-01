using ResultKit;

public class ExtensionTests
{
    [Fact]
    public void Map_Should_Transform_Value_When_Success()
    {
        Result<int> res = Result<int>.Success(10);
        var mapped = res.Map(x => x * 2);
        Assert.True(mapped.IsSuccess);
        Assert.Equal(20, mapped.Value);
    }

    [Fact]
    public void Bind_Should_FlatMap_Result()
    {
        Result<int> res = Result<int>.Success(5);
        var bound = res.Bind(x => Result<string>.Success($"S:{x}"));
        Assert.True(bound.IsSuccess);
        Assert.Equal("S:5", bound.Value);
    }

    [Fact]
    public void Match_Should_Invoke_OnSuccess_For_Success()
    {
        var result = Result<string>.Success("abc");
        var output = result.Match(
            s => s.ToUpper(),
            (err, val) => "fail"
        );
        Assert.Equal("ABC", output);
    }

    [Fact]
    public void Match_Should_Invoke_OnFailure_For_Failure()
    {
        var result = Result<string>.Failure(new Error("C", "fail"));
        var output = result.Match(
            s => "success",
            (err, val) => err!.Code
        );
        Assert.Equal("C", output);
    }

    [Fact]
    public void Match_Should_Invoke_OnFailure_For_ValidationFailure()
    {
        var result = Result<string>.ValidationFailure(new[] { new ValidationError("f", "m") });
        var output = result.Match(
            s => "success",
            (err, val) => val!.First().Message
        );
        Assert.Equal("m", output);
    }
}
