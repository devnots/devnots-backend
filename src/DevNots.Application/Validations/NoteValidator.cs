using System;
using System.Collections.Generic;
using System.Text;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.Note;
using DevNots.Domain.Note;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class NoteValidator:AbstractValidator<NoteDto>
    {
        public NoteValidator()
        {
            RuleFor(rule => rule.Text)
                .NotEmpty().WithMessage(CustomMessage)
                .MaximumLength(300);
            RuleFor(rule => rule.Description)
                .NotEmpty().WithMessage(CustomMessage)
                .MaximumLength(50);
            RuleFor(rule => rule.Title)
                .NotEmpty().WithMessage(CustomMessage)
                .MaximumLength(50);
            RuleFor(rule => rule.CreatedAt)
                .NotEmpty().WithMessage(CustomMessage)
                .GreaterThan(DateTime.Now);
        }

        private string CustomMessage = "{PropertyName} can not be empty";
    }
}
