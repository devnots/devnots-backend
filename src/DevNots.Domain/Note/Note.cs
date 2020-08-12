using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    public class Note:AggregateRoot,INoteDetails
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Note(int UserId,string Text)
        {
            this.UserId = UserId;
            this.Text = Text;
        }
    }
}
