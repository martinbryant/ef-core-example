using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Name : ValueObject
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        // public static Result<Name> Create(string input)
        // {
        //     if(string.IsNullOrWhiteSpace(input))
        //         return Result.Failure<Name>(Errors.Name.)
        // }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}