using System;
using System.Collections.Generic;
using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Presistence;
using GigHub.Presistence.Repositories;
using GigHub.Presistence.Repository;
using GigHub.Tests.Extinctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Web.WebApi.Filter;

namespace GigHub.Tests.Persistence.Repository
{
    [TestClass]
    public class NotificationRepositoryTest
    {
        private NotificationRepository _notificationRepository;
        private Mock<DbSet<Notification>> _mockNotification;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotification = new Mock<DbSet<Notification>>();

            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Notification).Returns(_mockNotification.Object);


            _notificationRepository = new NotificationRepository(mockContext.Object);


        }

        [TestMethod]
        public void GetNewNotificationFor_IsRead_ShouldReturnEmpty()
        {
            var gig = new Gigs { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            var noti = new Notification(gig, NotificationType.GigCreate);

            _mockNotification.SetSource(new List<Notification>() { noti });

            var notication = _notificationRepository.GetNotifications("1");
        }

    }
}
