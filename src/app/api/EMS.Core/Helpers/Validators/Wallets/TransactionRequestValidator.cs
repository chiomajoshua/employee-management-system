namespace EMS.Core.Helpers.Validators.Wallets
{
    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}