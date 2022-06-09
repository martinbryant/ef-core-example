using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Address : ValueObject
    {
        public const int Max_Address_Line_Length = 15;
        public const int Max_PostCode_Length = 8;

        private Address(string add1, string add2, string add3, string add4, string pcode)
        {
            Address1 = add1;
            Address2 = add2;
            Address3 = add3;
            Address4 = add4;
            PostCode = pcode;
        }

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

        public static Result<Address, Error> Create(AddressDto addressDto)
        {
            if(addressDto is null)
                return Errors.General.ValueIsRequired(nameof(Address));
            
            if (string.IsNullOrWhiteSpace(addressDto.Address1))
                return Errors.General.ValueIsRequired(nameof(Address1));

            if (addressDto.Address1.Length > Max_Address_Line_Length)
                return Errors.General.ValueIsTooLong(nameof(Address1), addressDto.Address1);
            
            if (string.IsNullOrWhiteSpace(addressDto.Address2))
                return Errors.General.ValueIsRequired(nameof(Address2));

            if (addressDto.Address2.Length > Max_Address_Line_Length)
                return Errors.General.ValueIsTooLong(nameof(Address2), addressDto.Address2);

            var address3 = addressDto.Address3 ?? string.Empty;

            if (address3.Length > Max_Address_Line_Length)
                return Errors.General.ValueIsTooLong(nameof(Address3), addressDto.Address3);

            var address4 = addressDto.Address4 ?? string.Empty;

            if (address4.Length > Max_Address_Line_Length)
                return Errors.General.ValueIsTooLong(nameof(Address4), addressDto.Address4);
            
            if (string.IsNullOrWhiteSpace(addressDto.PostCode))
                return Errors.General.ValueIsRequired(nameof(Address1));

            if (addressDto.PostCode.Length > Max_PostCode_Length)
                return Errors.General.ValueIsTooLong(nameof(PostCode), addressDto.PostCode);

            return new Address(
                addressDto.Address1, 
                addressDto.Address2, 
                address3, 
                address4, 
                addressDto.PostCode);
        }
    }
}