using ef_core_example.Models;

namespace ef_core_example.Tests
{
    public class AddressTests
    {
        public AddressTests()
        {

        }

        [Theory(DisplayName = "[Address] - creates an address")]
        [MemberData(nameof(ValidAddressData))]
        public void ValidAddress(AddressDto addressDto)
        {
           var result = Address.Create(addressDto);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value.Address3);
            Assert.NotNull(result.Value.Address4);
        }

        public static IEnumerable<object[]> ValidAddressData { get 
        {
            var maxAddressLength    = 15;
            var maxAddress          = CreateString(maxAddressLength);

            var maxPostcodeLength   = 8;
            var maxPostcode         = CreateString(maxPostcodeLength);

            return new List<object[]>
            {
                new object[] { new AddressDto() { Address1 = maxAddress, Address2 = maxAddress, Address3 = maxAddress, Address4 = maxAddress, PostCode = maxPostcode } },
                new object[] { new AddressDto() { Address1 = maxAddress, Address2 = maxAddress, Address3 = null, Address4 = null, PostCode = maxPostcode } },
                new object[] { new AddressDto() { Address1 = maxAddress, Address2 = maxAddress, Address3 = maxAddress, Address4 = null, PostCode = maxPostcode } }
            };
        }
        }

        [Fact(DisplayName = "[Address] - Invalid dto")]
        public void InvalidAddressNull()
        {
           var addressDto   = default(AddressDto);
           var expected     = Errors.General.Value_Is_Required;

           var result       = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }

        [InlineData(null            , Errors.General.Value_Is_Required)]
        [InlineData(""              , Errors.General.Value_Is_Required)]
        [InlineData("    "          , Errors.General.Value_Is_Required)]
        [InlineData("1234567890123456", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Address] - Invalid Line 1")]
        public void InvalidAddressLine1(string line1, string expected)
        {
            var addressDto = new AddressDto()
            {
                Address1 = line1,
                Address2 = "Ok",
                Address3 = "Ok",
                Address4 = "Ok",
                PostCode = "OK"
            };
            
            var result = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }

        [InlineData(null            , Errors.General.Value_Is_Required)]
        [InlineData(""              , Errors.General.Value_Is_Required)]
        [InlineData("    "          , Errors.General.Value_Is_Required)]
        [InlineData("1234567890123456", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Address] - Invalid Line 2")]
        public void InvalidAddressLine2(string line2, string expected)
        {
            var addressDto = new AddressDto()
            {
                Address1 = "Ok",
                Address2 = line2,
                Address3 = "Ok",
                Address4 = "Ok",
                PostCode = "OK"
            };
            
            var result = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }

        [InlineData("1234567890123456", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Address] - Invalid Line 3")]
        public void InvalidAddressLine3(string line3, string expected)
        {
            var addressDto = new AddressDto()
            {
                Address1 = "Ok",
                Address2 = "Ok",
                Address3 = line3,
                Address4 = "Ok",
                PostCode = "OK"
            };
            
            var result = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }

        [InlineData("1234567890123456", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Address] - Invalid Line 4")]
        public void InvalidAddressLine4(string line4, string expected)
        {
            var addressDto = new AddressDto()
            {
                Address1 = "Ok",
                Address2 = "Ok",
                Address3 = "Ok",
                Address4 = line4,
                PostCode = "OK"
            };
            
            var result = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }

        [InlineData("123456789", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Address] - Invalid Line 4")]
        public void InvalidPostCode(string postcode, string expected)
        {
            var addressDto = new AddressDto()
            {
                Address1 = "Ok",
                Address2 = "Ok",
                Address3 = "Ok",
                Address4 = "Ok",
                PostCode = postcode
            };
            
            var result = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }

        public static string CreateString(int length)
        {
            var range     = Enumerable.Range(0, length)
                                        .Select(num => "X");
            return string.Join("", range);
        }

        public static AddressDto ValidAddressDto =>
            (AddressDto)AddressTests.ValidAddressData.FirstOrDefault()?.FirstOrDefault()!; 

    }
    
    
}