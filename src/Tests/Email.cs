using ef_core_example.Models;
using Xunit;

namespace ef_core_example.Tests
{
    public class EmailTests
    {
        public EmailTests()
        {

        }

        [InlineData("mail@mail.net", "mail@mail.net")]
        [InlineData("mail@mail.net     ", "mail@mail.net")]
        [InlineData("     mail@mail.net", "mail@mail.net")]
        [Theory]
        public void ValidEmail(string givenEmail, string expected)
        {
            var result = Email.Create(givenEmail);

            Assert.Equal(expected, result.Value);
        }

        [InlineData("mailmail.net", "value.is.invalid")]
        [InlineData("mailmail.net", "value.is.invalid")]
        [InlineData("mailmail.net", "value.is.invalid")]
        [Theory]
        public void InvalidEmail(string givenEmail, string expected)
        {
            var result = Email.Create(givenEmail);

            Assert.Equal(expected, result.Error.Code);
        }
    }
}