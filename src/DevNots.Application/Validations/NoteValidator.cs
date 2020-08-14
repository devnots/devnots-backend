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
            RuleFor(rule => rule.Text).MinimumLength(1);
        }
    }
}
