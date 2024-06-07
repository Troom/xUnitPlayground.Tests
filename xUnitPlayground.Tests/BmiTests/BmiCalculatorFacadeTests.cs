using FluentAssertions;
using Moq;
using Xunit.Abstractions;
using xUnitPlayground.Library.Bmi;
using xUnitPlayground.Library.Model;

namespace xUnitPlayground.Tests.BmiTests
{
    public class BmiCalculatorFacadeTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public BmiCalculatorFacadeTests(ITestOutputHelper outputHelper) { 
            _outputHelper = outputHelper;
        }

        private const string Normal_Summary = "Normal";
        private const string Overweight_Summary = "Overweight";
        private const string Obesity_Summary = "Obesity";
        private const string Underweight_Summary = "Underweight";
        [Theory]
        [InlineData(50, 150)]
        public void GetResult_ForValidInput_ReturnsExpectedSummary(double weight, double height)
        {
            //Arrange
            var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            bmiDeterminatorMock
                    .Setup(x => x.DetermineBmi(It.IsAny<double>()))
                    .Returns(BmiClassification.Normal);
            BmiCalculatorFacade facade = new(UnitSystem.Metric, bmiDeterminatorMock.Object);
            //Act
            var result = facade.GetResult(weight, height);
            //Assert
            Assert.Equal(22.22, result.Bmi);
            Assert.Equal(BmiClassification.Normal, result.BmiClassification);
            Assert.Equal(Normal_Summary, result.Summary);
        }

        [Theory]
        [InlineData(BmiClassification.Overweight, Overweight_Summary)]
        [InlineData(BmiClassification.Normal, Normal_Summary)]
        [InlineData(BmiClassification.Obesity, Obesity_Summary)]
        [InlineData(BmiClassification.Underweight, Underweight_Summary)]
        public void GetResultWithFluentAssertionsAndMoq_ForValidInput_ReturnsExpectedSummary(BmiClassification classification, string summary)
        {
            //Arrange
            _outputHelper.WriteLine("Sample output from GetResultWithFluentAssertionsAndMoq_ForValidInput_ReturnsExpectedSummary");
            _outputHelper.WriteLine($"Tested values: \n Classification: {classification}\n Summary: {summary}");

            var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            bmiDeterminatorMock
                    .Setup(x => x.DetermineBmi(It.IsAny<double>()))
                    .Returns(classification);
            BmiCalculatorFacade facade = new(UnitSystem.Metric, bmiDeterminatorMock.Object);
            //Act
            var result = facade.GetResult(1, 1);
            //Assert
            result.Summary.Should().Be(summary);
        }
    }
}
