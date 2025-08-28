using FluentValidation;
using PhoneDirectory.Application.DTOs.Contact;

namespace PhoneDirectory.Application.Validators
{
    public class UpdateContactDTOValidator : AbstractValidator<UpdateContactDTO>
    {
        public UpdateContactDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID gereklidir.");

            RuleFor(x => x).SetValidator(new CreateContactDTOValidator());
        }
    }
}