﻿using System;

namespace Todo.Repositories.Models
{
    public class Note
    {
        public Guid? Id { get; set; }

        public string Content { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public Domain.Note ToDomain()
        {
            return new Domain.Note
            {
                Id = Id,
                Content = Content,
                CreateDate = CreateDate
            };
        }

        public static Note FromDomain(Domain.Note note)
        {
            return new Note
            {
                Id = note.Id,
                Content = note.Content,
                CreateDate = note.CreateDate
            };
        }
    }
}