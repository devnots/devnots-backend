using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.Keyword;
using DevNots.Application.Contracts.Note;
using DevNots.Application.Contracts.User;
using DevNots.Domain;
using DevNots.Domain.Keyword;
using DevNots.Domain.Note;

namespace DevNots.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<NoteDto, Note>().ReverseMap();
            CreateMap<KeywordDto, Keyword>().ReverseMap();
        }
    }
}
