using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    public class Note:AggregateRoot,INoteDetails
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public DateTime CreatedAt { get; set; }

        public Note(string UserId,string Text,string Title , string Description,string Keywords)
        {
            this.UserId = UserId;
            this.Text = Text;
            this.Title = Title;
            this.Description = Description;
            this.Keywords = Keywords;
        }
    }
}
