using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    interface INoteDetails
    {
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
