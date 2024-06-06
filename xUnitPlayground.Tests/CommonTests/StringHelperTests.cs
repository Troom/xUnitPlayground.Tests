using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //Arrange
            //Act
            //Assert
            Assert.Equal(expected, StringHelper.ReverseWords(input));
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
            //Assert
            Assert.Equal(expected, StringHelper.IsPalindrome(input));
        }

    }
}
