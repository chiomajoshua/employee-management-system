namespace EMS.Core.Services.Wallets.Implementation
{
    public class WalletService : IWalletService
    {
        private readonly IRepository _repository;
        private readonly IIdentityService _identityService;
        public WalletService(IRepository repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<IEnumerable<TransactionResponse>> BulkPostPayment(List<TransactionRequest> transactionRequests)
        {
            var response = new List<TransactionResponse>();
            IDbContextTransaction transaction = await _repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {                
                foreach (var item in transactionRequests)
                {
                    var transactionDate = DateTime.Now;
                    var employee = await GetEmployeeWallet(item.EmployeeId);
                    employee.Balance += item.Amount;
                    employee.UpdatedById = Guid.Parse(_identityService.GetUserId());
                    employee.UpdatedAt = transactionDate;

                    await _repository.UpdateAsync(employee);

                    response.Add(new TransactionResponse { EmployeeId = item.EmployeeId });
                }
                await transaction.CommitAsync();

                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<decimal> GetBalance()
        {
            var result = await GetEmployeeWallet(_identityService.GetUserId());
            return result.Balance;
        }       

        public async Task<TransactionResponse> PostPayment(TransactionRequest transactionRequest)
        {
            IDbContextTransaction transaction = await _repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var transactionDate = DateTime.Now;
                var employee = await GetEmployeeWallet(transactionRequest.EmployeeId);
                employee.Balance += transactionRequest.Amount;
                employee.UpdatedById = Guid.Parse(_identityService.GetUserId());
                employee.UpdatedAt = transactionDate;

                await _repository.UpdateAsync(employee);

                await transaction.CommitAsync();

                return new TransactionResponse { EmployeeId = transactionRequest.EmployeeId };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<Wallet> GetEmployeeWallet(string employeeId)
        {
            return await _repository.GetAsync<Wallet>(x => x.Employee.Id == employeeId);
        }
    }
}