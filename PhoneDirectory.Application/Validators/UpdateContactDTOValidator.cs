using System;
using System.Collections.Generic;
using System.Text;
using PhoneDirectory.Application.DTOs.Contact;

namespace PhoneDirectory.Application.Validators
{
    public class UpdateContactDTOValidator : IValidator<UpdateContactDTO>
    {
        public ValidationResult Validate(UpdateContactDTO dto)
        {
            var result = new ValidationResult();

            if (dto.Id <= 0)
                result.Errors.Add("Id must be a positive number.");

            var createValidator = new CreateContactDTOValidator();
            var createValidation = createValidator.Validate(dto);

            result.Errors.AddRange(createValidation.Errors);
            return result;
        }
    }
}
