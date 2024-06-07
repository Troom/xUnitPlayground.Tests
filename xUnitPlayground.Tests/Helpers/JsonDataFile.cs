using Newtonsoft.Json;
using System.Reflection;
using Xunit.Sdk;

namespace xUnitPlayground.Tests.xUnitPlayground.Tests.Helpers
{
    public class JsonDataFile : DataAttribute
    {
        private readonly string _jsonPath;

        public JsonDataFile(string jsonPath)
        {
            _jsonPath = jsonPath;
        }
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));


            var currentDir = Directory.GetCurrentDirectory();
            var jsonPath = Path.GetRelativePath(currentDir, _jsonPath);


            if (!File.Exists(jsonPath))
                throw new ArgumentException($"File not found on path {jsonPath}");

            var jsonData = File.ReadAllText(jsonPath);
            var result = JsonConvert.DeserializeObject<IEnumerable<object[]>>(jsonData);

            return result;
        }
    }
}
