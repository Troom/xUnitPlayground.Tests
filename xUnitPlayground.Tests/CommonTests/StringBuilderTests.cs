using System.Text;

namespace xUnitPlayground.Tests.CommonTests
{
    public class StringBuilderTests
    {
        [Fact]
        public void Append_ForTwoString_ReturnsConcatenatedString()
        {
            //Arrange
            StringBuilder sb = new();
            //Act
            sb.Append("test").Append("test2");
            string result = sb.ToString();

            //Assert
            Assert.Equal("testtest2", result);
        }
    }
}
