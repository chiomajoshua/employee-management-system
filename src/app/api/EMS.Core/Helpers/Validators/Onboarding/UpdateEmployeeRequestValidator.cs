namespace EMS.Core.Helpers.Validators.Onboarding
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.LastName).NotEmpty().NotNull().NotEqual("string");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().NotEqual("string");
        }
    }
}