using CommonArea.Models.Requests;
using CommonArea.Models.Responses;

namespace CommonArea.Interfaces
{
    public interface IManagerCourier
    {
        Task<CreateVoucherResponse> CreateVoucher(CreateVoucherRequest createVoucherRequest);
        Task<CancelVoucherResponse> CancelVoucher(CancelVoucherRequest cancelVoucherRequest);
        CreateVoucherResponse MapCreateVoucherResponse<T>(dynamic obj);
        CancelVoucherResponse MapCancelVoucherResponse<T>(dynamic obj);
    }
}
