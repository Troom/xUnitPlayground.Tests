namespace xUnitPlayground.Library.Common
{
    public static class ListHelper
    {
        public static List<int> FilterOddNumber(List<int> listOfNumbers)
        {
            var result = new List<int>();
            for (int i = 0; i < listOfNumbers.Count; i++)
            {
                if (listOfNumbers[i] % 2 != 0)
                {
                    result.Add(listOfNumbers[i]);
                }
            }
            return result;
        }
    }
}
