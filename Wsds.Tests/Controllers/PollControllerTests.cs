using Xunit;
using Moq;
using System.Collections.Generic;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wsds.WebApp.Auth;
using Wsds.WebApp.WebExtensions;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Security.Claims;
using System.Threading;

namespace Wsds.Tests.Controllers
{
    public class PollControllerTests
    {
        private Mock<IPollRepository> _mockIPollRepository;
        private PollController _pollController;

        public PollControllerTests()
        {
            _mockIPollRepository = new Mock<IPollRepository>();
            _pollController = new PollController(_mockIPollRepository.Object);
        }

        [Fact]
        public void PollControllerCreated()
        {
            Assert.NotNull(_pollController);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetPollTest(long id)
        {
            var poll = new Poll_DTO();
            _mockIPollRepository.Setup(x => x.GetPollById(It.IsAny<long>())).Returns(poll);

            var result = _pollController.GetPoll(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIPollRepository.Verify(x => x.GetPollById(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetPollQuestionsTest(long id)
        {
            var list = new List<PollQuestion_DTO> { new PollQuestion_DTO() };
            _mockIPollRepository.Setup(x => x.GetPollQuestionsByPollId(It.IsAny<long>())).Returns(list);

            var result = _pollController.GetPollQuestions(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIPollRepository.Verify(x => x.GetPollQuestionsByPollId(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetPollAnswersTest(long id)
        {
            var list = new List<PollQuestionAnswer_DTO> { new PollQuestionAnswer_DTO() };
            _mockIPollRepository.Setup(x => x.GetPollAnswersByQuestionId(It.IsAny<long>())).Returns(list);

            var result = _pollController.GetPollAnswers(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIPollRepository.Verify(x => x.GetPollAnswersByQuestionId(id), Times.Once);
        }
    }
}
