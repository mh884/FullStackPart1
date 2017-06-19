
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.API;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extinctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Contollers.Api
{
    [TestClass]
    public class GigContollerTest
    {
        private GigsController _gigsController;
        private Mock<IGigRespository> _mockRepository;
        private string _userid;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRespository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);
            _gigsController = new GigsController(mockUoW.Object);
            _userid = "1";
            _gigsController.MockCurrentUser(_userid, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();

        }
        [TestMethod]
        public void Cancel_GigUsCancel_ShouldReturnNotFound()
        {
            var gig = new Gigs();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGis_ShouldRreturnUnAuterized()
        {


            var gig = new Gigs { ArtistId = _userid + "-" };
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();

        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOkay()
        {
            var gig = new Gigs() { ArtistId = _userid };
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<OkResult>();

        }

    }
}
