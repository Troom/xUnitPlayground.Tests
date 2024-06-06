namespace xUnitPlayground.Library.Calculators
{
    public class ImperialBmiCalculator : IBmiCalculator
    {
        public double CalculateBmi(double weight, double height)
        {
            if (weight < 0)
                throw new ArgumentException("Weight is not valid");
            if (height < 0)
                throw new ArgumentException("Height is not valid");

            var bmi = weight / Math.Pow(height, 2) * 703;
            return Math.Round(bmi, 2);
        }

    }
}
