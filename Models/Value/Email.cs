using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Email : ValueObject
    {
        public const int Max_Email_Length = 256;
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Email, Error>(Errors.General.ValueIsRequired(nameof(Email)));

            string email = input.Trim();

            if (email.Length > Max_Email_Length)
                return Result.Failure<Email, Error>(Errors.General.ValueIsTooLong(nameof(Email), input));

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email, Error>(Errors.General.ValueIsInvalid(nameof(Email), input));

            return Result.Success<Email, Error>(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email) => email.Value;
    }
}