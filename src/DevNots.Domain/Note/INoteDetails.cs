using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    public interface INoteDetails
    {
        public string UserId { get; set; }
        public string Text { get; set; }
    }
}
