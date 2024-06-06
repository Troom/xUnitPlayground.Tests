# Unit Tests for xUnit

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
Theory
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


