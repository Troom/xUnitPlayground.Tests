using Newtonsoft.Json;
using System.Collections;

namespace xUnitPlayground.Tests.xUnitPlayground.Tests.BmiTests.TestData
{
    public class MetricBmiCalculatorTestsData : IEnumerable<object[]>
    {
        private const string JSON_PATH = "xUnitPlayground.Tests/BmiTests/TestData/MetricBmiCalculatorTestsData.json";

        public IEnumerator<object[]> GetEnumerator()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var jsonPath = Path.GetRelativePath(currentDir, JSON_PATH);


            if (!File.Exists(jsonPath))
                throw new ArgumentException($"File not found on path {jsonPath}");
        
            var jsonData = File.ReadAllText(jsonPath);
            var result = JsonConvert.DeserializeObject<IEnumerable<object[]>>(jsonData);

            return result!.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
