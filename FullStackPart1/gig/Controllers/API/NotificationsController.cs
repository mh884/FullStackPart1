
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _iUnitOfWork = unitOfWork;

        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            var notification = _iUnitOfWork.Notification.GetNotifications(User.Identity.GetUserId());


            return notification.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult NotificationClearRead()
        {
            var notification = _iUnitOfWork.Notification.GetUserNotifications(User.Identity.GetUserId());

            notification.ForEach(a => a.Read());

            _iUnitOfWork.Complate();

            return Ok();
        }

    }
}
