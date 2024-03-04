using System.Net;
using Identity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wakeclub.Data;
using Wakeclub.Entities;
using Wakeclub.Exceptions;
using Wakeclub.Models;

namespace Wakeclub.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "https://api.sandbox.hit-pay.com/v1/";
    public CustomerController(
        DataContext context,
        UserManager<User> userManager,
        HttpClient httpClient)
    {
        _context = context;
        _userManager = userManager;
        _httpClient = httpClient;
    }
    
    [HttpPatch("register")]
    public async Task<ActionResult<Customer>> RegisterNewCustomer(NewCustomerRequest newCustomerRequest)
    {
        // get user
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            throw new UserDoesNotExistException();
        }
        
        // create Wallet
        var wallet = new Wallet();
        
        // create customer
        var customer = new Customer(userId, user.Email, newCustomerRequest.PhoneNumber, newCustomerRequest.BankBranchCode,
            newCustomerRequest.BankId, newCustomerRequest.BankAccountNumber);
        customer.updateWallet(wallet);
        
        // update DB
        await _context.AddAsync<Customer>(customer);
        await _context.AddAsync<Wallet>(wallet);
        await _context.SaveChangesAsync();
        
        return Ok("Registration completed!");
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateUser(UpdateCustomerRequest updateCustomerRequest)
    {
        // get customer
        var userId = _userManager.GetUserId(User);
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == userId);
        
        // validate user
        if (customer == null)
        {
            throw new CustomerDoesNotExistException();
        }
        
        // update details
        customer.updatePhoneNumber(updateCustomerRequest.PhoneNumber); 
        customer.updateBankDetails(updateCustomerRequest.BankBranchCode, updateCustomerRequest.BankId, updateCustomerRequest.BankAccountNumber);
        await _context.SaveChangesAsync();

        return Ok("Details have been updated. ");

    }

    // public async Task<IActionResult> CreateBeneficiary()
    // {
    //     
    // }

}