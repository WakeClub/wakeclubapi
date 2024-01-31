using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wakeclub.Data;
using Wakeclub.Entities;
using Wakeclub.Exceptions;
using Wakeclub.Models;

namespace Wakeclub.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;
    public UserController(
        DataContext context,
        UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    [HttpPatch]
    [Route("/register")]
    public async Task<ActionResult<User>> RegisterNewUser(NewUser newUser)
    {
        // get user
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        // validate new user
        if (user == null)
        {
            throw new UserDoesNotExistException("Identity registration failed.");
        }
        
        if (user.BankDetails != null && user.Wallet != null)
        {
            throw new UserAlreadyRegisteredException();
        }
        
        // add phone number
        user.PhoneNumber = newUser.PhoneNumber;
        
        // create Wallet
        var wallet = new Wallet();
        user.Wallet = wallet;
        await _context.AddAsync<Wallet>(wallet);
        
        // create BankDetails
        var bankDetails = new BankDetails(newUser.PhoneNumber);
        user.BankDetails = bankDetails;
        await _context.AddAsync<BankDetails>(bankDetails);
        
        await _context.SaveChangesAsync();
        
        return Ok(user);
    }

    [HttpPatch]
    [Route("/update")]
    public async Task<IActionResult> UpdateUser(ClientUser clientUser)
    {
        // get user
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        
        // validate user
        if (user == null)
        {
            throw new UserDoesNotExistException();
        }
        
        // update details
        user.PhoneNumber = clientUser.PhoneNumber;
        await _context.SaveChangesAsync();

        return Ok("Details have been updated. ");

    }

}