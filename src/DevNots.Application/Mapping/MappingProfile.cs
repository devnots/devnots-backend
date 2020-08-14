using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.Note;
using DevNots.Application.Contracts.User;
using DevNots.Domain;
using DevNots.Domain.Note;

namespace DevNots.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<NoteDto, Note>().ReverseMap();
        }
    }
}
