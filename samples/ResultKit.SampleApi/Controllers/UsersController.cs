using Microsoft.AspNetCore.Mvc;

namespace ResultKit.SampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<UserDto> _users = new List<UserDto>
    {
        new UserDto { Id = 1, Name = "Test User 1" },
        new UserDto { Id = 2, Name = "Test User 2" }
    };

    [HttpGet("{id}")]
    public ActionResult<Result<UserDto>> GetUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return Result<UserDto>.Failure(new Error(ErrorCodes.NotFound, $"User not found for id: {id}")).ToActionResult();
        return Result<UserDto>.Success(user).ToActionResult();
    }

    [HttpPost]
    public ActionResult<Result<UserDto>> CreateUser(UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return Result<UserDto>.ValidationFailure(new[] { new ValidationError(nameof(dto.Name), "Name is required.") }).ToActionResult();
        dto.Id = _users.Count > 0 ? _users.Max(x => x.Id) + 1 : 1;
        _users.Add(dto);
        return Result<UserDto>.Success(dto).ToActionResult();
    }

    [HttpPut("{id}")]
    public ActionResult<Result<UserDto>> UpdateUser(int id, UserDto dto)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return Result<UserDto>.Failure(new Error(ErrorCodes.NotFound, $"User not found for id: {id}")).ToActionResult();
        if (string.IsNullOrWhiteSpace(dto.Name))
            return Result<UserDto>.ValidationFailure(new[] { new ValidationError(nameof(dto.Name), "Name is required.") }).ToActionResult();
        user.Name = dto.Name;
        return Result<UserDto>.Success(user).ToActionResult();
    }

    [HttpGet]
    public ActionResult<Result<List<UserDto>>> GetAll()
    {
        return Result<List<UserDto>>.Success(_users).ToActionResult();
    }

    [HttpGet("implicit-action")]
    public ActionResult<Result<List<UserDto>>> GetAllImplicitAction()
    {
        Result<List<UserDto>> result = _users;
        return result.ToActionResult();
    }


    [HttpGet("implicit")]
    public Result<List<UserDto>> GetAllImplicit()
    {
        return _users;
    }


}

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
