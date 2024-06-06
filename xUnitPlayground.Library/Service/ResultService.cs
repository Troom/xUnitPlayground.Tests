using xUnitPlayground.Library.Bmi;
using xUnitPlayground.Library.Model;

namespace xUnitPlayground.Library.Service
{
    public class ResultService
    {
        public BmiResult RecentNormalResult { get; private set; }
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository) {
            _resultRepository = resultRepository;
        }

        public void SetRecentNormalResult(BmiResult result) {
            if (result.BmiClassification == BmiClassification.Normal)
            {
                RecentNormalResult = result;
            }
        }

        public async Task SaveUnderweightResultAsync(BmiResult result)
        {
            if (result.BmiClassification == BmiClassification.Normal)
            {
               await _resultRepository.SaveResultAsync(result);
            }
        }
    }
}
