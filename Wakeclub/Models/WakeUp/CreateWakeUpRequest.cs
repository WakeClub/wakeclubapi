namespace Wakeclub.Models;

public class CreateWakeUpRequest
{
    public DateTimeOffset WakeUpTime { get; set; }
    public decimal Amount { get; set; }
}