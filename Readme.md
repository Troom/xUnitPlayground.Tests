### Useful shortcuts
#### Visual Studio
`ctrl +r, crtl + t` -> test with debug
#### CLI
### Basics
```csharp
[Fact]
public void MethodName_Scenario_ExpectedResult()
{
	//Arrange
	//Act
	///Assert
}
```

#### Multiple arguments
```csharp
[Theory]
[InlineData(1,2)]
[InlineData(2,3)]
```

### Testing Exceptions
```csharp
[Theory]
[InlineData(2, 0)]
[InlineData(2, -2)]
public void CalculateBmi_WithHeightLessOrEqualZero_ThrowArgumentException(double weight, double height)
{
	//Arrange
	MetricBmiCalculator calculator = new MetricBmiCalculator();
	////Act
	Action action = () => calculator.CalculateBmi(weight, height);
	////Assert
	Assert.Throws<ArgumentException>(action);
}
```
### Testing objects (assertion for every result property)

```csharp
private const string Normal_Summary = "Normal";

[Theory]
[InlineData(50, 150)]
public void GetResult_ForValidInput_ReturnsExpectedSummary(double weight, double height)
{
	//Arrange
	BmiCalculatorFacade facade = new(Library.Model.UnitSystem.Metric);
	//Act
	var result = facade.GetResult(weight, height);
	//Assert
	Assert.Equal(22.22, result.Bmi);
	Assert.Equal(BmiClassification.Overweight, result.BmiClassification);
	Assert.Equal(Normal_Summary, result.Summary);
}

```
### Fluent Assertions

* Require get package from nuget.

Syntax:

```csharp
result.Bmi.Should().Be(22.22);
result.BmiClassification.Should().Be(BmiClassification.Normal);
result.Summary.Should().Be(Normal_Summary);
```



### Moq

Why mock object
* To limit times of testing methods (we should test method only one especially when the methods requires a lot of time to be executed)
* Enter when dependency injection method are used.


```csharp
[Fact]
public void GetResultWithFluentAssertionsAndMoq_ForValidInput_ReturnsExpectedSummary()
{
	//Arrange
	var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
	bmiDeterminatorMock
			.Setup(x => x.DetermineBmi(It.IsAny<double>()))
			.Returns(BmiClassification.Normal);
	BmiCalculatorFacade facade = new(UnitSystem.Metric, bmiDeterminatorMock.Object);
	//Act
	var result = facade.GetResult(1, 1);
	//Assert
	result.Summary.Should().Be(Normal_Summary);
}
```

Can be refacored, but in that case it's not worth it.

### Testing void methods

Void methods can be splited into two category:
Setter -> when method change state of object.
Action -> when method "do something". (for example save data in database)


#### Setter example
```csharp
[Fact]
public void SetRecentNormalResult_NormalResult_RecentNormalResultIsNormal()
{
	//Arrange
	var resultService = new ResultService(_resultRepository.Object);
	var bmiResult = new BmiResult() { BmiClassification= BmiClassification.Normal };
	//Act
	resultService.SetRecentNormalResult(bmiResult);
	//Assert
	resultService.RecentNormalResult.Should().Be(bmiResult);
}
```


#### Actions example
```csharp
[Fact]
public async Task SaveUnderweightResultAsync_ForNormalClassification_InvokeSaveResultAsync()
{
	//Arrange 
	var resultService = new ResultService(_resultRepository.Object);
	var bmiResult = new BmiResult() 
		{ BmiClassification = BmiClassification.Normal };
	
	await resultService.SaveUnderweightResultAsync(bmiResult);
	_resultRepository.Verify(mock=> mock.SaveResultAsync(bmiResult), Times.Once);
}
```

### Passing Collections into unitTests

InlineData from xUnit can consume only argument which are constat (e.g. we can't create new list). So in this case we are forced to use walkaround that mean we can create List with object in our test class, and next pass index of desired item into inlineData. For example:

```csharp
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
public void ValidateOverlapping_ForNotOverlappingDataRanges_ReturnsFalse (int index)
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

```


## Usefull Tools

### MemberData

We can refactor this

```csharp

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
  public void ValidateOverlapping_ForOverlappingDataRanges_ReturnsFalse (int index)
  {
  //NIY
  }
```

Into

```csharp

private static IEnumerable<object[]? GetRangeList(){
yield return new object[]{
         new List<DateRange>()
             {
                 new DateRange(new DateTime(2020, 1, 1), new DateTime(2020, 1, 5)),
                 new DateRange(new DateTime(2020, 2, 1), new DateTime(2020, 2, 5)),
             },
};
yield return new object[]{
         new List<DateRange>()
             {
              new DateRange(new DateTime(2020, 1, 10), new DateTime(2020, 1, 25)),
             },
};

yield return new object[]{
         new List<DateRange>()
             {
                new DateRange(new DateTime(2020, 1, 3), new DateTime(2020, 1, 17)),
             },
};

yield return new object[]{
         new List<DateRange>()
             {
                new DateRange(new DateTime(2020, 1, 7), new DateTime(2020, 1, 9))
             },
};
}

[Theory]
[MemberData(nameof(GetRangeList))]
  public void ValidateOverlapping_ForOverlappingDataRanges_ReturnsFalse (List<DateRange> ranges)
  {
  //NIY
  }
```
### ClassData

Similar to MemberData but for me it's more clean soultion. To use this you should special static class and import for class
```csharp
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
```

Usage:
```csharp
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
```
### TestOutputHelper

Help to attach some output to our tests

#### Requirements

Inject ITestOutputHelper into constructor
```csharp
private readonly ITestOutputHelper _outputHelper;

public BmiCalculatorFacadeTests(ITestOutputHelper outputHelper) { 
	_outputHelper = outputHelper;
}
```

#### Usage
```csharp
_outputHelper.WriteLine("Sample output from GetResultWithFluentAssertionsAndMoq_ForValidInput_ReturnsExpectedSummary");
_outputHelper.WriteLine($"Tested values: \n Classification: {classification}\n Summary: {summary}");
```
#### Result
![[Pasted image 20240607140338.png]]
### Tested cases from JSON file

#### Requrements
1. Attach file to project
For Example: 
```json
  [
    [ 50, 150, 22.22 ],
    [ 60, 160, 23.44 ],
    [ 70, 170, 24.22 ],
    [ 80, 180, 24.69 ],
    [ 90, 190, 24.93 ]
  ]
```
**Remember to copy file to `Output Directory`**

2. Create class for deserialization
```csharp
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
```
#### Usage
```csharp
   [Theory]
   [ClassData(typeof(MetricBmiCalculatorTestsData))]
   public void CalculateBmiFromJson_WithProperInput_ReturnsExpectedOutput(double weight, double height, double exptected)
   {
       //Arrange
       MetricBmiCalculator calculator = new MetricBmiCalculator();
       //Act
       var result = calculator.CalculateBmi(weight, height);
       //Assert
       Assert.Equal(exptected, result);
   }
```

### Custom attribute DataMember

#### Requirements
We should inherit from DataAttribute. Below example for adding json files
```csharp
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
```

#### Usage
```csharp
[Theory][JsonDataFile("xUnitPlayground.Tests/BmiTests/TestData/MetricBmiCalculatorTestsData.json")]
public void CalculateBmiFromJsons_WithProperInput_ReturnsExpectedOutput(double weight, double height, double exptected)
{
	//Arrange
	MetricBmiCalculator calculator = new MetricBmiCalculator();
	//Act
	var result = calculator.CalculateBmi(weight, height);
	//Assert
	Assert.Equal(exptected, result);
}
```