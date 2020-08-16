using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    public interface INoteDetails
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Keywords { get; set; }

    }
}
