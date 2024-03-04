using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wakeclub.Data;
using Wakeclub.Entities;
using Wakeclub.Services;

namespace Wakeclub.Controllers;


[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class WakeClubPoolController: ControllerBase
{
    private readonly DataContext _context;
    public WakeClubPoolController(
        DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPool()
    {
        var wakeClubPool = new WakeClubPool();
        var date = DateTimeOffset.UtcNow.Date;
        
        await _context.AddAsync<WakeClubPool>(wakeClubPool);
        await _context.SaveChangesAsync();
        return Ok("Wake club pool created for " + date.ToString("dd-mm-yyyy"));
    }

    [HttpPatch("add-stake")]
    public async Task<IActionResult> AddStake(decimal amount)
    {
        var currDate = DateTimeOffset.UtcNow.Date;
        if (!DateTimeComparison.IsCurrentTimeBetween8PMAnd12AM())
        {
            throw new Exception("Invalid time to stake");
        }

        var wakeClubPool = await _context.WakeClubPools.FirstOrDefaultAsync(x => x.Date == currDate);
        if (wakeClubPool == null)
        {
            throw new Exception("WakeClubPool not found");
        }

        wakeClubPool.StakedAmount += amount;
        await _context.SaveChangesAsync();

        return Ok("Amount added: " + amount.ToString());
    }

    [HttpPatch("add-loss")]
    public async Task<IActionResult> AddLoss(decimal amount)
    {
        var wakeUpPoolDate = DateTime.Now.Date.AddDays(-1);
        var wakeClubPool = await _context.WakeClubPools.FirstOrDefaultAsync(x => x.Date == wakeUpPoolDate);
        if (wakeClubPool == null)
        {
            throw new Exception("WakeClubPool not found");
        }

        wakeClubPool.LostAmount += amount;
        await _context.SaveChangesAsync();

        return Ok("Loss amount added: " + amount.ToString());
    }
    
}