using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Keyword
{
    public class Keyword:AggregateRoot,IKeywordDetails
    {
        public string Title { get; set; }

        public Keyword(string title)
        {
            Title = title;
        }
    }
}
