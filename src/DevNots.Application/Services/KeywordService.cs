using AutoMapper;
using DevNots.Application.Contracts.Keyword;
using DevNots.Domain;
using DevNots.Domain.Keyword;
using DevNots.Domain.Note;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.Note;

namespace DevNots.Application.Services
{
    public class KeywordService:AppService
    {
        private readonly IKeywordRepository keywordRepository;
        private readonly IMapper mapper;
        private readonly IValidator<KeywordDto> validator;

        public KeywordService(IKeywordRepository keywordRepository, IMapper mapper, IValidator<KeywordDto> validator)
        {
            this.keywordRepository = keywordRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<AppResponse<string>> CreateAsync(KeywordDto aggregate)
        {
            var validationResult = validator.Validate(aggregate);                                                                    
            var response = new AppResponse<string>();

            if (validationResult.Errors.Count > 0)
            {
                var errorMessage = validationResult.Errors.SingleOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var _keyword = mapper.Map<Keyword>(aggregate);
            var id = await keywordRepository.CreateAsync(_keyword);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteKeywordAsync(DeleteKeywordDto request)
        {
            var response = new AppResponse();

            var isSuccess = await keywordRepository.RemoveAsync(request.Id);

            if (!isSuccess)
            {
                var errorMessage = "Keyword not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            return response;
        }
        public async Task<AppResponse<IEnumerable<KeywordDto>>> GetKeywordsAsync(KeywordListDto request)
        {
            var response = new AppResponse<IEnumerable<KeywordDto>>();

            if (request.Limit > 49 || request.Limit < 1)
            {
                var errorMessage = "limit must between 1-50";
                return ErrorResponse(errorMessage, 400, response);
            }

            return await PaginateAsync(1, request.Limit);
        }

        public async Task<AppResponse<IEnumerable<KeywordDto>>> PaginateAsync(int page, int pageSize)
        {
            var response = new AppResponse<IEnumerable<KeywordDto>>();

            if (pageSize > 49)
            {
                var errorMessage = "pageSize can not be greater than 50";
                return ErrorResponse(errorMessage, 400, response);
            }

            var keywords = await keywordRepository.PaginateAsync(page, pageSize);

            response.Result = mapper.Map<IEnumerable<KeywordDto>>(keywords);
            return response;
        }

        public async Task<AppResponse<bool>> UpdateNoteAsync(KeywordDto request)
        {
            var validationResult = validator.Validate(request);
            var response = new AppResponse<bool>();

            if (validationResult.Errors.Any())
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            if (string.IsNullOrEmpty(request.Id))
                return ErrorResponse("Id can not be empty.", 400, response);

            var keyword = mapper.Map<Keyword>(request);
            var isUpdated = await keywordRepository.UpdateAsync(request.Id, keyword);

            response.Result = isUpdated;
            return response;
        }
    }
}
