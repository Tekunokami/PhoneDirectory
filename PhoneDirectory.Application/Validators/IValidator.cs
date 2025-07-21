using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Application.Validators
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T entity);
    }

    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new List<string>();
    }
}
