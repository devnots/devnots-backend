using DevNots.Domain.Keyword;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Application.Contracts.Keyword
{
    public class KeywordDto:IKeywordDetails
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
