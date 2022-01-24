using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Abstraction
{
    public interface INoteData
    {
        /// <summary>
        /// Should be set by the repository
        /// </summary>
        Guid? Id { get; }

        string Content { get; }
        DateTimeOffset CreateDate { get; }
    }
}