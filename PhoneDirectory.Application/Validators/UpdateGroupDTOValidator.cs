using FluentValidation;
using PhoneDirectory.Application.DTOs.Group;

namespace PhoneDirectory.Application.Validators
{
    public class UpdateGroupDTOValidator : AbstractValidator<UpdateGroupDTO>
    {
        public UpdateGroupDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir grup ID'si gereklidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Grup adý boþ olamaz.")
                .MaximumLength(50).WithMessage("Grup adý en fazla 50 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açýklama en fazla 200 karakter olabilir.");
        }
    }
}