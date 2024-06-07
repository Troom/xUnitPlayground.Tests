using FluentAssertions;
using xUnitPlayground.Library.Common;
using xUnitPlayground.Tests.xUnitPlayground.Tests.CommonTests.TestData;

namespace xUnitPlayground.Tests.xUnitPlayground.Tests.CommonTests
{
    public class DataRangeValidatorTests
    {
        public static IEnumerable<object[]> GetRangeList()
        {
            yield return new object[]{
                new List<DateRange>()
                    {
                        new DateRange(new DateTime(2020, 1, 1), new DateTime(2020, 1, 5)),
                        new DateRange(new DateTime(2020, 2, 1), new DateTime(2020, 2, 5)),
                    }
            };
            yield return new object[]{
                new List<DateRange>()
                    {
                    new DateRange(new DateTime(2020, 1, 10), new DateTime(2020, 1, 25)),
                    }
            };

            yield return new object[]{
                new List<DateRange>()
                    {
                    new DateRange(new DateTime(2020, 1, 3), new DateTime(2020, 1, 17)),
                    }
            };

            yield return new object[]{
                new List<DateRange>()
                    {
                    new DateRange(new DateTime(2020, 1, 7), new DateTime(2020, 1, 9))
                    }
            };
        }
        [Theory]
        [MemberData(nameof(GetRangeList))]
        public void ValidateOverlapping_ForOverlappingDataRangesMemberData_ReturnsFalse(List<DateRange> dataRanges)
        {
            //Arrange
            var dataRangeValidator = new DataRangeValidator();
            var input = new DateRange(new DateTime(2020, 1, 5), new DateTime(2020, 1, 15));
            //Act
            var result = dataRangeValidator.ValidateOverlapping(dataRanges, input);
            //Assert
            result.Should().BeFalse();
        }
        [Theory]
        [ClassData(typeof(ValidatorTestData))]
        public void ValidateOverlapping_ForOverlappingDataRangesClassData_ReturnsFalse(List<DateRange> dataRanges)
        {
            //Arrange
            var dataRangeValidator = new DataRangeValidator();
            var input = new DateRange(new DateTime(2020, 1, 5), new DateTime(2020, 1, 15));
            //Act
            var result = dataRangeValidator.ValidateOverlapping(dataRanges, input);
            //Assert
            result.Should().BeFalse();
        }

        private List<List<DateRange>> _rangeList = new List<List<DateRange>>() {
         new List<DateRange>()
             {
                 new DateRange(new DateTime(2020, 1, 1), new DateTime(2020, 1, 5)),
                 new DateRange(new DateTime(2020, 2, 1), new DateTime(2020, 2, 5)),
             },
        new List<DateRange>()
             {
                 new DateRange(new DateTime(2020, 1, 10), new DateTime(2020, 1, 25)),
             },
        new List<DateRange>()
             {
                 new DateRange(new DateTime(2020, 1, 3), new DateTime(2020, 1, 17)),
             },
        new List<DateRange>()
             {
                 new DateRange(new DateTime(2020, 1, 7), new DateTime(2020, 1, 9)),
             }
        };

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ValidateOverlapping_ForOverlappingDataRangesInlineData_ReturnsFalse (int index)
        {
            //Arrange
            var dataRangeValidator = new DataRangeValidator();
            var input = new DateRange(new DateTime(2020, 1, 5), new DateTime(2020, 1, 15));
            var dataRanges = _rangeList[index];
            //Act
            var result = dataRangeValidator.ValidateOverlapping(dataRanges, input);
            //Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void ValidateOverlapping_ForNonOverlappingDataRanges_ReturnsFalse(int index)
        {
            //Arrange
            var dataRangeValidator = new DataRangeValidator();
            var input = new DateRange(new DateTime(2020, 1, 5), new DateTime(2020, 1, 15));
            var dataRanges = _rangeList[index];
            //Act
            var result = dataRangeValidator.ValidateOverlapping(dataRanges, input);
            //Assert
            result.Should().BeFalse();
        }
    }
}
