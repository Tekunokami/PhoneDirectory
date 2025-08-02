using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using PhoneDirectory.Application.DTOs.Contact;

namespace PhoneDirectory.Application.Validators
{
    public class CreateContactDTOValidator : IValidator<CreateContactDTO>
    {
        public ValidationResult Validate(CreateContactDTO dto)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(dto.Name))
                result.Errors.Add("Name is required.");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                result.Errors.Add("Phone number is required.");
            else if (dto.PhoneNumber.Length < 10)
                result.Errors.Add("Phone number must be at least 10 digits.");

            if (!string.IsNullOrWhiteSpace(dto.Email) && !dto.Email.Contains("@"))
                result.Errors.Add("Email must contain '@' symbol.");

            if (!string.IsNullOrWhiteSpace(dto.Birthday))
            {
                if (DateTime.TryParseExact(dto.Birthday, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    if (parsedDate > DateTime.UtcNow)
                        result.Errors.Add("Birth date cannot be in the future.");
                }
                else
                {
                    result.Errors.Add("Invalid birth date format. Please use dd.MM.yyyy.");
                }
            }

            return result;
        }
    }
}
