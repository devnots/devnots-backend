using System;
using System.Collections.Generic;
using System.Text;
using DevNots.Application.Contracts.Keyword;
using DevNots.Domain.Keyword;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class KeywordValidator:AbstractValidator<KeywordDto>
    {
        public KeywordValidator()
        {
            RuleFor(x => x.Title).MaximumLength(30).WithMessage(CustomMessage);
        }
        private string CustomMessage = "{PropertyName} can not be empty";

    }
}
