using System.Collections;
using xUnitPlayground.Library.Common;

namespace xUnitPlayground.Tests.xUnitPlayground.Tests.CommonTests.TestData
{
    public class ValidatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
