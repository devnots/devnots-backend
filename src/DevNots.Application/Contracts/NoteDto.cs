using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Application.Contracts
{
    public class NoteDto
    {
        public int Id { get; internal set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
