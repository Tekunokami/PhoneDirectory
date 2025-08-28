using FluentValidation;
using PhoneDirectory.Application.DTOs.Group;

namespace PhoneDirectory.Application.Validators
{
    public class UpdateGroupDTOValidator : AbstractValidator<UpdateGroupDTO>
    {
        public UpdateGroupDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Ge�erli bir grup ID'si gereklidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Grup ad� bo� olamaz.")
                .MaximumLength(50).WithMessage("Grup ad� en fazla 50 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("A��klama en fazla 200 karakter olabilir.");
        }
    }
}