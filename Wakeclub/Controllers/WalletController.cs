using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
using Wakeclub.Services;

namespace Wakeclub.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "https://api.sandbox.hit-pay.com/v1/";
    private IPaynowDepositService _paynowDepositService;
    private ILogger<WalletController> _logger;
    public WalletController(
        DataContext context,
        UserManager<User> userManager,
        HttpClient httpClient,
        IPaynowDepositService paynowDepositService,
        ILogger<WalletController> logger)
    {
        _context = context;
        _userManager = userManager;
        _httpClient = httpClient;
        _paynowDepositService = paynowDepositService;
        _logger = logger;
    }
    
    [Authorize]
    [HttpPost("paynow/create")]
    public async Task<IActionResult> CreateDeposit(CreatePaynowDepositRequest createPaynowDepositRequest)
    {
        // get user
        var userId = _userManager.GetUserId(User);
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == userId);
        
        // validate user
        if (customer == null)
        {
            throw new CustomerDoesNotExistException();
        }

        customer = customer as Customer;

        var response = await _paynowDepositService.CreatePaynowDeposit(createPaynowDepositRequest.Amount, customer.Email);
        
        // create transaction
        var transaction = new Transaction(response.ReferenceNumber, TransactionType.DEPOSIT, TransactionStatus.PENDING,
            decimal.Parse(createPaynowDepositRequest.Amount), PaymentRail.PAYNOW, "SGD", customer.PhoneNumber,
            customer.Email);
        transaction.updateCustomer(customer);
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        
        return Ok(response);
    }

    [HttpPost("paynow/webhook")]
    public async Task<IActionResult> ReceivePaynowDepositWebhook(
        [FromForm] ReceivePaynowDepositWebhookRequest request)
    {
        var verified = _paynowDepositService.verify(request);

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(x => x.Id == request.reference_number);
        
        if (!verified)
        {
            // created failed transaction
            var failedTransaction = new Transaction(transaction.Id, transaction.TransactionType, TransactionStatus.FAILED_AUTHENTICATION_HMAC, transaction.Amount,
                transaction.PaymentMethod, transaction.Currency, transaction.PhoneNumber, transaction.Email);
            failedTransaction.updateCustomer(transaction.Customer);
            await _context.AddAsync<Transaction>(failedTransaction);
            await _context.SaveChangesAsync();
            throw new PaynowDepositHmacAuthenticationFailedException();
        }
        
        // created updated transaction
        var newTransaction = new Transaction(transaction.Id, transaction.TransactionType, TransactionStatus.SUCCESS, transaction.Amount,
            transaction.PaymentMethod, transaction.Currency, transaction.PhoneNumber, transaction.Email);
        newTransaction.updateCustomer(transaction.Customer);
        await _context.AddAsync<Transaction>(newTransaction);
        await _context.SaveChangesAsync();
        
        return Ok("Validation succeeded");
    }
    
}