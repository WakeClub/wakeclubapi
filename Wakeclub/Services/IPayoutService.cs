using Wakeclub.Models.Omise;

namespace Wakeclub.Services;

public interface IPayoutService
{
    public Task<CreateBeneficiaryResponse> CreateBeneficiary(CreateBeneficiaryRequest request);
}