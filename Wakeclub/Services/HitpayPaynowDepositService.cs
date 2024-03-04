using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Wakeclub.Exceptions;
using Wakeclub.Models;

namespace Wakeclub.Services;

public class HitpayPaynowDepositService : IPaynowDepositService
{
    private readonly HttpClient _httpClient;
    public HitpayPaynowDepositService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<CreatePaynowDepositResponse> CreatePaynowDeposit(string amount, string email)
    {
        // get time 30 min from now
        TimeZoneInfo singaporeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
        DateTime currentSingaporeTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, singaporeTimeZone);

        // Add 30 minutes to the current time
        DateTime newTime = currentSingaporeTime.AddMinutes(30);

        // Format the new time as "yyyy-MM-dd HH:mm:ss"
        string formattedTime = newTime.ToString("yyyy-MM-dd HH:mm:ss");
        
        // create transaction
        var request = new HttpRequestMessage(HttpMethod.Post, GlobalConstants.HitpayAPIUrl + "payment-requests");
        request.Headers.Add("X-BUSINESS-API-KEY", GlobalConstants.HitpayAPIKey);
        var referenceId = Guid.NewGuid().ToString();
        var body = new[]
        {
            new KeyValuePair<string, string>("amount", amount),
            new KeyValuePair<string, string>("payment_methods[]", "paynow_online"),
            new KeyValuePair<string, string>("currency", "SGD"),
            new KeyValuePair<string, string>("email", "leesengkittkitt@gmail.com"),
            new KeyValuePair<string, string>("expiry_date", formattedTime),
            new KeyValuePair<string, string>("webhook", GlobalConstants.BaseUrl + "api/v1/wallet/paynow/webhook"),
            new KeyValuePair<string, string>("reference_number", referenceId),
        };
        request.Content = new FormUrlEncodedContent(body);
        var response = await _httpClient.SendAsync(request);
        if (response.StatusCode != HttpStatusCode.Created)
        {
            throw new PaynowDepositRequestFailedException("Status Code: " + response.StatusCode + "\n" + response.Content.ReadAsStringAsync());
        }
        // map response
        var responseContent = await response.Content.ReadAsStringAsync();
        var mappedResponse = JsonConvert.DeserializeObject<CreatePaynowDepositResponse>(responseContent);
        return mappedResponse;
    }

    public bool verify(ReceivePaynowDepositWebhookRequest request)
    {
        // validate webhook request
        PropertyInfo[] properties = typeof(ReceivePaynowDepositWebhookRequest).GetProperties();
        Array.Sort(properties, (x, y) => 
            string.Compare(x.Name, y.Name, StringComparison.Ordinal));
        StringBuilder stringBuilder = new StringBuilder();

        foreach (var property in properties)
        {
            if (property.Name != "Hmac") // Exclude the Hmac key
            {
                object? value = property.GetValue(request);
                stringBuilder.Append($"{property.Name}{value}");
            }
        }
        var validateString = stringBuilder.ToString().Replace(" ", "");
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(GlobalConstants.HitpaySalt));
        var computedHmac = hmac.
            ComputeHash(Encoding.UTF8.GetBytes(validateString));
        var computedHmacString = BitConverter.ToString(computedHmac).Replace("-", "").ToLower();

        return computedHmacString == request.Hmac;
    }
}