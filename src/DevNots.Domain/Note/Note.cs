using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    public class Note:AggregateRoot,INoteDetails
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Note(string UserId,string Text)
        {
            this.UserId = UserId;
            this.Text = Text;
        }
    }
}
