using System;
using Domain.Data.Abstraction;

namespace Todo.Domain
{
    /// <summary>
    /// A note either exists or does not, we will not track deleting/archiving
    /// </summary>
    public class Note : INoteData
    {
        /// <summary>
        /// Should be set by the repository
        /// </summary>
        public Guid? Id { get; set; }

        public string Content { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}