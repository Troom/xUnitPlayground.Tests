using xUnitPlayground.Library.Calculators;

namespace xUnitPlayground.Tests.BmiTests
{
    public class MetricBmiCalculatorTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(-2, 1)]
        public void CalculateBmi_WithWeighLessOrEqualZero_ThrowArgumentException(double weight, double height)
        {
            //Arrange
            MetricBmiCalculator calculator = new MetricBmiCalculator();
            ////Act
            Action action = () => calculator.CalculateBmi(weight, height);
            ////Assert
            Assert.Throws<ArgumentException>(action);
        }
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

        [Theory]
        [InlineData(50, 150, 22.22)]
        [InlineData(60, 160, 23.44)]
        [InlineData(70, 170, 24.22)]
        [InlineData(80, 180, 24.69)]
        [InlineData(90, 190, 24.93)]
        public void CalculateBmi_WithProperInput_ReturnsExpectedOutput(double weight, double height, double exptected)
        {
            //Arrange
            MetricBmiCalculator calculator = new MetricBmiCalculator();
            //Act
            var result = calculator.CalculateBmi(weight, height);
            //Assert
            Assert.Equal(exptected, result);
        }
    }
}
