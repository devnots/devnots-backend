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
            var validatitonResult = validator.Validate(userDto);
            var response = new AppResponse<string>();

            if (validatitonResult.Errors.Any())
            {
                var errorMessage = validatitonResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var user = mapper.Map<User>(userDto);
            var id = await userRepository.CreateAsync(user);

            response.Result = id;
            return response;
        }

    }
}
