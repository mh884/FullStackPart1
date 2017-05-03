
using AutoMapper;
using GigHub.Controllers.API;
using GigHub.Dto;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {


            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<gig, gigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();

        }
    }
}