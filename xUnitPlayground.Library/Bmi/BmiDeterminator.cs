namespace xUnitPlayground.Library.Bmi
{
    public class BmiDeterminator : IBmiDeterminator
    {
        public BmiClassification DetermineBmi(double bmi) => bmi switch
        {
            < 18.5 => BmiClassification.Underweight,
            < 25.0 => BmiClassification.Normal,
            <= 30.0 => BmiClassification.Overweight,
            _ => BmiClassification.Obesity
        };
    }
}