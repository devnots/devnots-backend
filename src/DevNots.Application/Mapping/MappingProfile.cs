using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Domain;

namespace DevNots.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
