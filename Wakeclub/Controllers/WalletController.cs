using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wakeclub.Data;
using Wakeclub.Entities;

namespace Wakeclub.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;
    
    public WalletController(
        DataContext context,
        UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // [HttpPost]
    // [Route("/create-transaction")]
    // public Task<IActionResult> CreateTransaction()
    // {
    //     return Ok("hi");
    // }
}