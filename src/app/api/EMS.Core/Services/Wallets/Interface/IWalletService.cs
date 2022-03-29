namespace EMS.Core.Services.Wallets.Interface
{
    public interface IWalletService : IAutoDependencyCore
    {
        Task<decimal> GetBalance(string employeeId);
        Task<TransactionResponse> PostPayment(TransactionRequest transactionRequest);
        Task<IEnumerable<TransactionResponse>> BulkPostPayment(List<TransactionRequest> transactionRequests);
    }
}