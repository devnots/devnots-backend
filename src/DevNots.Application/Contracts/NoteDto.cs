using System;
using System.Collections.Generic;
using System.Text;
using DevNots.Domain.Note;

namespace DevNots.Application.Contracts
{
    public class NoteDto : INoteDetails
    {
        public string Id { get; internal set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
