using FluentValidation;
using PhoneDirectory.Application.DTOs.Contact;
using System;
using System.Globalization;

namespace PhoneDirectory.Application.Validators
{
    public class CreateContactDTOValidator : AbstractValidator<CreateContactDTO>
    {
        public CreateContactDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .MinimumLength(10).WithMessage("Telefon numarası en az 10 haneli olmalıdır.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Birthday)
                .Cascade(CascadeMode.Stop) 
                .Must(BeAValidDate).WithMessage("Doğum günü formatı geçersiz. Lütfen dd.MM.yyyy kullanın.")
                .Must(NotBeInTheFuture).WithMessage("Doğum günü gelecekteki bir tarih olamaz.")
                .When(x => !string.IsNullOrWhiteSpace(x.Birthday));
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool NotBeInTheFuture(string date)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.Date <= DateTime.UtcNow.Date;
            }
            return true;
        }
    }
}