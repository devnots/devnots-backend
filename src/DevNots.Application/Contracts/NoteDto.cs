using System;
using System.Collections.Generic;
using System.Text;
using DevNots.Domain.Note;

namespace DevNots.Application.Contracts
{
    public class NoteDto : INoteDetails
    {
        public int Id { get; internal set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
