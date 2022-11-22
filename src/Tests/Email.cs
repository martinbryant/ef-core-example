using ef_core_example.Models;

namespace ef_core_example.Tests
{
    public class EmailTests
    {
        public EmailTests()
        {

        }

        [InlineData("mail@mail.net")]
        [InlineData("mail@mail.net     ")]
        [InlineData("     mail@mail.net")]
        [InlineData("     mail@mail.net       ")]
        [Theory(DisplayName = "[Email] - creates an email")]
        public void ValidEmail(string givenEmail)
        {
            var expected = "mail@mail.net";
            var result = Email.Create(givenEmail);

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, result.Value);
        }

        [InlineData("mailmail.net"  , Errors.General.Value_Is_Invalid)]
        [InlineData(null            , Errors.General.Value_Is_Required)]
        [InlineData(""              , Errors.General.Value_Is_Required)]
        [InlineData("    "          , Errors.General.Value_Is_Required)]
        [InlineData("12345678901234567890123456", Errors.General.Value_Is_Too_Long)]
        [Theory(DisplayName = "[Email] - Invalid email")]
        public void InvalidEmail(string givenEmail, string expected)
        {
            var result = Email.Create(givenEmail);

            Assert.Equal(expected, result.Error.Code);
        }
    }
}