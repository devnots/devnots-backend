using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.Note;
using DevNots.Application.Contracts.User;
using DevNots.Application.Mapping;
using DevNots.Application.Repositories;
using DevNots.Application.Services;
using DevNots.Application.Validations;
using DevNots.Domain;
using DevNots.Domain.Note;
using DevNots.MongoDb;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevNots.Application.Extensions
{
    /// <summary>
    /// Contains extension methods for configuring the application services and features.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all necessary services and dependencies to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            return services
                .AddSingleton(mapper)
                .AddScoped<IValidator<UserDto>, UserValidator>()
                .AddScoped<UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IValidator<NoteDto>, NoteValidator>()
                .AddScoped<NoteService>()
                .AddScoped<INoteRepository,NoteRepository>();
        }

        public static IServiceCollection AddMongoDb(this IServiceCollection services, string connectionStrig)
        {
            var dbContext = new MongoDbContext(connectionStrig);

            return services
                .AddSingleton<DbContext>(dbContext);
        }
    }
}
