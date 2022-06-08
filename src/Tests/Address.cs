using ef_core_example.Models;

namespace ef_core_example.Tests
{
    public class AddressTests
    {
        public AddressTests()
        {

        }

        [InlineData("mail@mail.net")]
        [InlineData("mail@mail.net     ")]
        [InlineData("     mail@mail.net")]
        [Fact(DisplayName = "[Address] - creates an address")]
        public void ValidAddress()
        {
            var addressDto = new AddressDto()
            {
                Address1 = "Ok",
                Address2 = "Ok",
                Address3 = "Ok",
                Address4 = "Ok",
                PostCode = "OK"
            };
            
            var result = Address.Create(addressDto);

            Assert.True(result.IsSuccess);
        }

        [InlineData("mailmail.net"  , Errors.General.Value_Is_Invalid)]
        [InlineData(null            , Errors.General.Value_Is_Required)]
        [InlineData(""              , Errors.General.Value_Is_Required)]
        [InlineData("    "          , Errors.General.Value_Is_Required)]
        [InlineData("12345678901234567890123456", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Address] - Invalid Line 1")]
        public void InvalidAddress(string givenEmail, string expected)
        {
            var addressDto = new AddressDto()
            {
                Address1 = "Ok",
                Address2 = "Ok",
                Address3 = "Ok",
                Address4 = "Ok",
                PostCode = "OK"
            };
            
            var result = Address.Create(addressDto);

            Assert.Equal(expected, result.Error.Code);
        }
    }
}