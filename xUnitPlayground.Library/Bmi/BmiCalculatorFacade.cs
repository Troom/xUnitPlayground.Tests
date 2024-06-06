using xUnitPlayground.Library.Calculators;
using xUnitPlayground.Library.Model;

namespace xUnitPlayground.Library.Bmi
{
    public class BmiCalculatorFacade
    {
        private readonly UnitSystem _unitSystem;
        private readonly IBmiCalculator _bmiCalculator;
        private readonly IBmiDeterminator _bmiDeterminator;
        public BmiCalculatorFacade(UnitSystem unitSystem, IBmiDeterminator bmiDeterminator)
        {
            _unitSystem = unitSystem;
            _bmiCalculator = GetBmiCalculator(unitSystem);
            _bmiDeterminator = bmiDeterminator;
        }

        private IBmiCalculator GetBmiCalculator(UnitSystem unitSystem)
        {
            switch (unitSystem)
            {
                case UnitSystem.Metric:
                    return new MetricBmiCalculator();
                case UnitSystem.Imperial:
                    return new ImperialBmiCalculator();
                default:
                    throw new ArgumentException("Invalid unit system");
            }
        }
        private string GetSummary(BmiClassification classification)
            => classification switch
            {
                BmiClassification.Obesity => "Obesity",
                BmiClassification.Overweight => "Overweight",
                BmiClassification.Normal => "Normal",
                BmiClassification.Underweight => "Underweight",
                _ => throw new ArgumentException("Invalid classification")
            };


        public BmiResult GetResult(double weight, double height)
        {
            var bmi = _bmiCalculator.CalculateBmi(weight, height);
            var classification = _bmiDeterminator.DetermineBmi(bmi);

            return new BmiResult()
            {
                Bmi = bmi,
                BmiClassification = classification,
                Summary = GetSummary(classification)
            };
        }
    }
}
