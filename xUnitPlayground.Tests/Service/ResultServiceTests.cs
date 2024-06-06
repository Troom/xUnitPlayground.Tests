using xUnitPlayground.Library.Model;
using xUnitPlayground.Library.Service;
using FluentAssertions;
using Moq;
using xUnitPlayground.Library.Bmi;

namespace xUnitPlayground.Tests.Service
{
    public class ResultServiceTests
    {
        private Mock<IResultRepository> _resultRepository;
        public ResultServiceTests() {
            _resultRepository = new Mock<IResultRepository>();
            _resultRepository.Setup(x => x.SaveResultAsync(It.IsAny<BmiResult>())).Returns(Task.FromResult(1));
        }

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

        [Fact]
        public void SetRecentNormalResult_NonNormalResult_RecentNormalResultIsNonNormal()
        {
            //Arrange
            var resultService = new ResultService(_resultRepository.Object);
            var bmiResult = new BmiResult() { BmiClassification = BmiClassification.Obesity };
            //Act
            resultService.SetRecentNormalResult(bmiResult);
            //Assert
            resultService.RecentNormalResult.Should().BeNull();
        }

        [Fact]
        public async Task SaveUnderweightResultAsync_ForNormalClassification_InvokeSaveResultAsync()
        {
            //Arrange 
            var resultService = new ResultService(_resultRepository.Object);
            var bmiResult = new BmiResult() { BmiClassification = BmiClassification.Normal };

            await resultService.SaveUnderweightResultAsync(bmiResult);

            _resultRepository.Verify(mock=> mock.SaveResultAsync(bmiResult), Times.Once);
        }
        [Fact]
        public async Task SaveUnderweightResultAsync_ForNonNormalClassification_DoNotInvokeSaveResultAsync()
        {
            //Arrange 
            var resultService = new ResultService(_resultRepository.Object);
            var bmiResult = new BmiResult() { BmiClassification = BmiClassification.Obesity };

            await resultService.SaveUnderweightResultAsync(bmiResult);

            _resultRepository.Verify(mock => mock.SaveResultAsync(bmiResult), Times.Never);
        }
    }
}

//public async Task SaveUnderweightResultAsync(BmiResult result)
//{
//    if (result.BmiClassification == BmiClassification.Normal)
           