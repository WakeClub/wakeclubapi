using Microsoft.AspNetCore.Mvc;

namespace Wakeclub.Models;

public class ReceivePaynowDepositWebhookRequest
{
    [FromForm(Name = "payment_id")]
    public string? payment_id { get; set; }
    
    [FromForm(Name = "payment_request_id")]
    public string? payment_request_id { get; set; }

    [FromForm(Name = "reference_number")]
    public string? reference_number { get; set; }

    [FromForm(Name = "phone")]
    public string? phone { get; set; }
    
    [FromForm(Name = "amount")]
    public string? amount { get; set; }
    
    [FromForm(Name = "currency")]
    public string? currency { get; set; }

    [FromForm(Name = "status")]
    public string? status { get; set; }

    [FromForm(Name = "hmac")]
    public string? Hmac { get; set; }

}