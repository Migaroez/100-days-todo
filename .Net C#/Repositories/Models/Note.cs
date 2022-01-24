using System;
using Domain.Data.Abstraction;
using Todo.Domain.Factories;

namespace Todo.Repositories.Models
{
    public class Note : INoteData
    {
        public Guid? Id { get; set; }

        public string Content { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public Domain.Note ToDomain()
        {
            return NoteFactory.CreateFrom(this);
        }

        public static Note FromDomain(Domain.Note note)
        {
            return new Note
            {
                // We do not copy the Id
                Content = note.Content,
                CreateDate = note.CreateDate
            };
        }

        public Note UpdateFromDomain(Domain.Note note)
        {
            Content = note.Content;
            CreateDate = note.CreateDate;
            return this;
        }
    }
}