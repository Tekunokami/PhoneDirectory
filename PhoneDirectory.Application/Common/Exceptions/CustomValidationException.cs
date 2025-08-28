using System;
using System.Collections.Generic;

namespace PhoneDirectory.Application.Common.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> Errors { get; }

        public CustomValidationException(List<string> errors)
            : base(string.Join("; ", errors))
        {
            Errors = errors;
        }
    }
}