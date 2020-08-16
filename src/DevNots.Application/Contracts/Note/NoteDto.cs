using System;
using System.Collections.Generic;
using DevNots.Domain.Note;

namespace DevNots.Application.Contracts.Note
{
    public class NoteDto : INoteDetails
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
