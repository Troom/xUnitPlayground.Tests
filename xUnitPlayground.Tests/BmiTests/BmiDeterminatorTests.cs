using xUnitPlayground.Library.Bmi;

namespace xUnitPlayground.Tests.BmiTests
{
    public class BmiDeterminatorTests
    {
        //[Fact]
        [Theory]
        [InlineData(10.0, BmiClassification.Underweight)]
        [InlineData(12.0, BmiClassification.Underweight)]
        [InlineData(16.0, BmiClassification.Underweight)]
        [InlineData(20.0, BmiClassification.Normal)]
        [InlineData(22.0, BmiClassification.Normal)]
        [InlineData(24.0, BmiClassification.Normal)]
        [InlineData(26.0, BmiClassification.Overweight)]
        [InlineData(27.0, BmiClassification.Overweight)]
        [InlineData(28.0, BmiClassification.Overweight)]
        [InlineData(32.0, BmiClassification.Obesity)]
        [InlineData(34.0, BmiClassification.Obesity)]
        [InlineData(38.0, BmiClassification.Obesity)]
        public void DetermineBmi_ForGivenBmi_ReturnsCorrectClassification(double bmi, BmiClassification classification)
        {
            //Arrange
            BmiDeterminator bmiDeterminator = new();
            //Act
            BmiClassification result = bmiDeterminator.DetermineBmi(bmi);
            //Assert
            Assert.Equal(classification, result);
        }




    }
}