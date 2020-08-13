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

        public async Task<AppResponse<string>> CreateNoteAsync(NoteDto noteDto)
        {
            var validationResult = validator.Validate(noteDto);
            var response = new AppResponse<string>();

            if (validationResult.Errors.Count > 0)
            {
                var errorMessage = validationResult.Errors.SingleOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var _note = mapper.Map<Note>(noteDto);
            var id = await _INoteRepository.CreateAsync(_note);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteNoteAsync(DeleteNoteDto request)
        {
            var response = new AppResponse();

            var isSuccess = await _INoteRepository.RemoveAsync(request.Id);

            if (!isSuccess)
            {
                var errorMessage = "Note not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            return response;
        }

        public async Task<AppResponse<IEnumerable<NoteDto>>> GetNotesAsync(NoteListDto request)
        {
            var response = new AppResponse<IEnumerable<NoteDto>>();

            if (request.Limit > 49 || request.Limit < 1)
            {
                var errorMessage = "limit must between 1-50";
                return ErrorResponse(errorMessage, 400, response);
            }

            return await PaginateAsync(1, request.Limit);
        }

        public async Task<AppResponse<IEnumerable<NoteDto>>> PaginateAsync(int page, int pageSize)
        {
            var response = new AppResponse<IEnumerable<NoteDto>>();

            if (pageSize > 49)
            {
                var errorMessage = "pageSize can not be greater than 50";
                return ErrorResponse(errorMessage, 400, response);
            }

            var users = await _INoteRepository.PaginateAsync(page, pageSize);

            response.Result = mapper.Map<IEnumerable<NoteDto>>(users);
            return response;
        }

        public async Task<AppResponse<string>> UpdateNoteAsync(NoteDto noteDto)
        {
            var response = new AppResponse<string>();

            var isSuccess = await _INoteRepository.FindAsync(x=>x.Id == noteDto.Id).ConfigureAwait(false);

            if (isSuccess.SingleOrDefault() == null)
            {
                var errorMessage = "Note not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            var _note = mapper.Map<Note>(noteDto);
            var id = await _INoteRepository.UpdateAsync(noteDto.Id, _note);

            return response;
        }
    }
}
