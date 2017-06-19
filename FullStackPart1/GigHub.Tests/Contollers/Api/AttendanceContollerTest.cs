using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.API;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extinctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Contollers.Api
{
    [TestClass]
    public class AttendanceContollerTest
    {
        private AttendancesController _attendancesController;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userid;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendance).Returns(_mockRepository.Object);
            _attendancesController = new AttendancesController(mockUoW.Object);
            _userid = "1";
            _attendancesController.MockCurrentUser(_userid, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_AddAttend_Ok()
        {
            var result = _attendancesController.Attend(new AttendanceDto { GigId = 1 });
            result.Should().BeOfType<OkResult>();

        }

        [TestMethod]
        public void Attend_ExistAttend_BadRequest()
        {


            var attendanceDto = new AttendanceDto { GigId = 1 };
            var attend = new List<Attendance> { new Attendance { gigId = 1, AttendeeId = "1" } };

            _mockRepository.Setup(a => a.GetAttendance("1", 1)).Returns(attend);
            var result = _attendancesController.Attend(attendanceDto);
            result.Should().BeOfType<BadRequestErrorMessageResult>();

        }


    }
}
