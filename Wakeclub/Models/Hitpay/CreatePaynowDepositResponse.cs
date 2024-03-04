using Newtonsoft.Json;

namespace Wakeclub.Models;

public class CreatePaynowDepositResponse
{
    [JsonProperty("id")]
    public string? Id { get; set; }
    
    [JsonProperty("name")]
    public string? Name { get; set; }
    
    [JsonProperty("email")]
    public string? Email { get; set; }
    
    [JsonProperty("phone")]
    public string? PhoneNumber { get; set; }
    
    [JsonProperty("amount")]
    public string? Amount { get; set; }
    
    [JsonProperty("currency")]
    public string? Currency { get; set; }
    
    [JsonProperty("status")]
    public string? Status { get; set; }
    
    [JsonProperty("purpose")]
    public string? Purpose { get; set; }
    
    [JsonProperty("reference_number")]
    public string? ReferenceNumber { get; set; }
    
    [JsonProperty("payment_methods")]
    public string[]? PaymentMethods { get; set; }
    
    [JsonProperty("url")]
    public string? Url { get; set; }
    
    [JsonProperty("redirect_url")]
    public string? RedirectUrl { get; set; }
    
    [JsonProperty("webhook")]
    public string? Webhook { get; set; }
    
    [JsonProperty("send_sms")]
    public bool? SendSms { get; set; }
    
    [JsonProperty("send_email")]
    public bool? SendEmail { get; set; }
    
    [JsonProperty("sms_status")]
    public string? SmsStatus { get; set; }
    
    [JsonProperty("email_status")]
    public string? EmailStatus { get; set; }
    
    [JsonProperty("allow_repeated_payments")]
    public bool? AllowRepeatedPayments { get; set; }
    
    [JsonProperty("expiry_date")]
    public string? ExpiryDate { get; set; }
    
    [JsonProperty("address")]
    public string? Address { get; set; }
    
    [JsonProperty("line_items")]
    public string? LineItems { get; set; }
    
    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string? UpdatedAt { get; set; }
}