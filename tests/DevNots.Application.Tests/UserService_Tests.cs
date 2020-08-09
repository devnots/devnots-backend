using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Mapping;
using DevNots.Application.Services;
using DevNots.Application.Validations;
using DevNots.Domain;
using Moq;
using Xunit;

namespace DevNots.Application.Tests
{
    public class UserService_Tests
    {
        private readonly IMapper mapper;
        public UserService_Tests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task RegisterAsync_WhenUserDetailsValid_ReturnsUserId()
        {
            // Arrange
            var validator = new UserValidator();
            var mockRepo = new Mock<IUserRepository>();

            mockRepo.Setup(x => x.CreateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult("id"));

            var userService = new UserService(mockRepo.Object, mapper, validator);

            var user = new UserDto()
            {
                Email    = "serkanbircan21@yandex.com",
                Username = "fasetto",
                Password = "MyPassword"
            };

            // Act
            var response = await userService.RegisterAsync(user);
            var id = response.Result;

            // Assert
            Assert.NotEmpty(id);
        }
    }
}
