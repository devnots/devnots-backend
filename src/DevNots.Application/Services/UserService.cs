using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Domain;
using FluentValidation;

namespace DevNots.Application.Services
{
    public class UserService: AppService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IValidator<UserDto> validator;
        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDto> validator)
        {
            this.mapper = mapper;
            this.validator = validator;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Registers given user to the database.
        /// </summary>
        /// <param name="userDto">User to register</param>
        /// <returns>Id of the registered user</returns>
        public async Task<AppResponse<string>> RegisterAsync(UserDto userDto)
        {
            var validationResult = validator.Validate(userDto);
            var response = new AppResponse<string>();

            if (validationResult.Errors.Any())
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var user = mapper.Map<User>(userDto);
            var id = await userRepository.CreateAsync(user);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteUserAsync(DeleteUserDto request)
        {
            var response = new AppResponse();

            var isSuccess = await userRepository.RemoveAsync(request.Id);

            if (!isSuccess)
            {
                var errorMessage = "User not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            return response;
        }

        public async Task<AppResponse<IEnumerable<UserDto>>> GetUsersAsync(UserListDto request)
        {
            var response = new AppResponse<IEnumerable<UserDto>>();

            if (request.Limit > 49 || request.Limit < 1)
            {
                var errorMessage = "limit must between 1-50";
                return ErrorResponse(errorMessage, 400, response);
            }

            return await PaginateAsync(1, request.Limit);
        }

        public async Task<AppResponse<IEnumerable<UserDto>>> PaginateAsync(int page, int pageSize)
        {
            var response = new AppResponse<IEnumerable<UserDto>>();

            if (pageSize > 49)
            {
                var errorMessage = "pageSize can not be greater than 50";
                return ErrorResponse(errorMessage, 400, response);
            }

            var users = await userRepository.PaginateAsync(page, pageSize);

            response.Result = mapper.Map<IEnumerable<UserDto>>(users);
            return response;
        }

    }
}
