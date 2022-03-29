namespace EMS.Core.Services.Wallets.Implementation
{
    public class WalletService : IWalletService
    {
        private readonly IRepository _repository;
        private readonly IEmployeeService _employeeService;
        private readonly IIdentityService _identityService;
        public WalletService(IRepository repository, IEmployeeService employeeService, IIdentityService identityService)
        {
            _repository = repository;
            _employeeService = employeeService;
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
                    var employee = await GetEmployee(item.EmployeeId);
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

        public async Task<decimal> GetBalance(string employeeId)
        {
            var result = await GetEmployee(employeeId);
            return result.Balance;
        }       

        public async Task<TransactionResponse> PostPayment(TransactionRequest transactionRequest)
        {
            IDbContextTransaction transaction = await _repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var transactionDate = DateTime.Now;
                var employee = await GetEmployee(transactionRequest.EmployeeId);
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

        private async Task<Wallet> GetEmployee(string employeeId)
        {
            return await _repository.GetAsync<Wallet>(x => x.Employee.Id == employeeId);
        }
    }
}