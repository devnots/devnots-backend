using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Domain;
using DevNots.Domain.Note;
using FluentValidation;

namespace DevNots.Application.Services
{
    public class NoteService:AppService
    {
        private readonly INoteRepository _INoteRepository;
        private readonly IMapper mapper;
        private readonly IValidator<NoteDto> validator;

        public NoteService(INoteRepository _INoteRepository, IMapper mapper, IValidator<NoteDto> validator)
        {
            this._INoteRepository = _INoteRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        //Many functions missing.
    }
}
