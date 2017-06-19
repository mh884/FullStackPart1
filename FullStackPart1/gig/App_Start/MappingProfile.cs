
using AutoMapper;
using GigHub.Controllers.API;
using GigHub.Core.Dto;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {


            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gigs, gigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();

        }
    }
}