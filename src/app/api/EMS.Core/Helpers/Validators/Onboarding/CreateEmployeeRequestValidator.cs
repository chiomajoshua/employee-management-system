namespace EMS.Core.Helpers.Validators.Onboarding
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.LastName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.Password).NotEmpty().NotNull().NotEqual("string");
        }
    }
}