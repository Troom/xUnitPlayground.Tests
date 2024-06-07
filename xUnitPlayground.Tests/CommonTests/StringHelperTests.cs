using FluentAssertions;
using xUnitPlayground.Library.Common;

namespace xUnitPlayground.Tests.CommonTests
{

    // "ala ma kota" => "kota ma ala"
    // "to jest test" -> "test jest to"
    public class StringHelperTests
    {
        [Theory]
        [InlineData("ala ma kota", "kota ma ala")]
        [InlineData("to jest test", "test jest to")]
        public void ReverseWords_SomeString_ReturnsReversedWordsInString(string input, string expected)
        {
            //Arrange -> from parameters
            //Act
            var result = StringHelper.ReverseWords(input);
            //Assert
            //Assert.Equal(expected, result);
            //or 
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("ola", false)]
        [InlineData("ala", true)]
        [InlineData("Ala", false)]
        [InlineData("", true)]
        public void IsPalindrome_SomeString_ReturnsExpectedResult(string input, bool expected)
        {
            //Arrange
            //Act
            var result = StringHelper.IsPalindrome(input);
            //Assert
            result.Should().Be(expected);
        }

    }
}
