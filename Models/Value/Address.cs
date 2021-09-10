using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Address : ValueObject
    {
        public const int Max_Address_Line_Length    = 33;
        public const int Max_PostCode_Length        = 8;

        private Address(string add1, string add2, string add3, string add4, string pcode)
        {
            Address1 = add1;
            Address2 = add2;
            Address3 = add3;
            Address4 = add4;
            PostCode = pcode;
        }

        public Address() {}

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Address4 { get; private set; }
        public string PostCode { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address1;
            yield return Address2;
            yield return Address3;
            yield return Address4;
            yield return PostCode;
        }

        public static Result<Address, Error> Create(string add1, string add2, string add3, string add4, string pcode)
        {
            if(string.IsNullOrWhiteSpace(add1))
                return Result.Failure<Address, Error>(Errors.General.ValueIsRequired(nameof(Address1)));
            
            if(string.IsNullOrWhiteSpace(add2))
                return Result.Failure<Address, Error>(Errors.General.ValueIsRequired(nameof(Address2)));

            if(string.IsNullOrWhiteSpace(pcode))
                return Result.Failure<Address, Error>(Errors.General.ValueIsRequired(nameof(Address1)));

            if(add1.Length > Max_Address_Line_Length)
                return Result.Failure<Address, Error>(Errors.General.ValueIsTooLong(nameof(Address1), add1));
            
            if(add2.Length > Max_Address_Line_Length)
                return Result.Failure<Address, Error>(Errors.General.ValueIsTooLong(nameof(Address2), add2));
            
            if(pcode.Length > Max_PostCode_Length)
                return Result.Failure<Address, Error>(Errors.General.ValueIsTooLong(nameof(PostCode), pcode));

            return Result.Success<Address, Error>(new Address(add1, add2, add3, add4, pcode));
        }
    }
}