using FluentAssertions;
using xUnitPlayground.Library.Common;

namespace xUnitPlayground.Tests.xUnitPlayground.Tests.CommonTests
{
    public class ListHelperTests
    {
        [Fact]
        public void FilterOddNumbers_ListWithOddNumbers_ReturnsListWithoutOddNumbers()
        {
            //Arrange
            var x = new List<int> { 1, 2, 3, 4,4,4, 5, 6, 7, 8, 9, 10 };
            //Act
            var result = ListHelper.FilterOddNumber(x);
            //Assert
            result.Should().Equal(new List<int> { 1, 3, 5, 7, 9 });
        }
        [Fact]
        public void FilterOddNumbers_ListWithoutOddNumbers_ReturnsTheSameList()
        {
            //Arrange
            var x = new List<int> { 1, 3, 5, 7, 9 };
            //Act
            var result = ListHelper.FilterOddNumber(x);
            //Assert
            result.Should().BeEquivalentTo(x);
        }
    }
}
