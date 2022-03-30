namespace EMS.Core.Services.Wallets.Interface
{
    public interface IWalletService : IAutoDependencyCore
    {
        Task<decimal> GetBalance();
        Task<TransactionResponse> PostPayment(TransactionRequest transactionRequest);
        Task<IEnumerable<TransactionResponse>> BulkPostPayment(List<TransactionRequest> transactionRequests);
        Task<Wallet> GetEmployeeWallet(string employeeId);
    }
}