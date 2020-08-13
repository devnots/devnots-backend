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
        private readonly INoteRepository noteRepository;
        private readonly IMapper mapper;
        private readonly IValidator<NoteDto> validator;

        public NoteService(INoteRepository noteRepository, IMapper mapper, IValidator<NoteDto> validator)
        {
            this.noteRepository = noteRepository;
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
            var id = await noteRepository.CreateAsync(_note);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteNoteAsync(DeleteNoteDto request)
        {
            var response = new AppResponse();

            var isSuccess = await noteRepository.RemoveAsync(request.Id);

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

            var users = await noteRepository.PaginateAsync(page, pageSize);

            response.Result = mapper.Map<IEnumerable<NoteDto>>(users);
            return response;
        }

        public async Task<AppResponse<bool>> UpdateNoteAsync(NoteDto noteDto)
        {
            var validationResult = validator.Validate(noteDto);
            var response = new AppResponse<bool>();

            if (validationResult.Errors.Any())
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            if (string.IsNullOrEmpty(noteDto.Id))
                return ErrorResponse("Id can not be empty.", 400, response);

            var note = mapper.Map<Note>(noteDto);
            var isUpdated = await noteRepository.UpdateAsync(noteDto.Id, note);

            response.Result = isUpdated;
            return response;
        }
    }
}
