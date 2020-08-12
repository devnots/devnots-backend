using System;
using System.Collections.Generic;
using System.Text;

namespace DevNots.Domain.Note
{
    public interface INoteRepository:IAsyncRepository<Note>
    {
    }
}
