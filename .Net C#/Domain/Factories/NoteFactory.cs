using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Data.Abstraction;

namespace Todo.Domain.Factories
{
    public  class NoteFactory
    {
        public static Note Create()
        {
            return new Note();
        }

        public static Note CreateFrom(INoteData noteData)
        {
            return new Note
            {
                Id = noteData.Id,
                Content = noteData.Content,
                CreateDate = noteData.CreateDate
            };
        }
    }
}
