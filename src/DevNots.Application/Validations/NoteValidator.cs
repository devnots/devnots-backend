using System;
using System.Collections.Generic;
using System.Text;
using DevNots.Domain.Note;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class NoteValidator:AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(rule => rule.Text).MinimumLength(1);
        }
    }
}
