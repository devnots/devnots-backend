using DevNots.Application.Contracts;
using DevNots.Application.Contracts.User;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class UserValidator: AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(CustomMessage)
                .MaximumLength(32);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(CustomMessage)
                .MaximumLength(32);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(CustomMessage)
                .EmailAddress();
        }
        private string CustomMessage = "{PropertyName} can not be empty";
    }
}
