using Microsoft.AspNetCore.Mvc;
using Wakeclub.Entities;

namespace Wakeclub.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return Ok("users");
    }
}