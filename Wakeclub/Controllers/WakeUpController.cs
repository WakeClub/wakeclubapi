using Identity.Entity;
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
[ApiController]
[Route("api/v1/[controller]")]
public class WakeUpController: ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;
    
    public WakeUpController(
        UserManager<User> userManager,
        DataContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost("create")]
    public async Task<ActionResult<WakeUp>> CreateWakeUp(CreateWakeUpRequest request)
    {
        var userId = _userManager.GetUserId(User);
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == userId);

        if (customer == null)
        {
            throw new CustomerDoesNotExistException();
        }

        var wakeUpPoolDate = DateTime.Now.Date;
        var wakeClubPool = await _context.WakeClubPools.FirstOrDefaultAsync(x => x.Date == wakeUpPoolDate);
        if (wakeClubPool == null)
        {
            throw new Exception("WakeClubPool not found");
        }

        var wakeUp = new WakeUp(
            request.Amount,
            request.WakeUpTime
        );
        wakeUp.updateCustomer(customer);
        wakeUp.updateWakeClubPool(wakeClubPool);

        await _context.WakeUps.AddAsync(wakeUp);
        wakeClubPool.StakedAmount += request.Amount;
        
        await _context.SaveChangesAsync();

        return Ok(wakeUp);
    }
}