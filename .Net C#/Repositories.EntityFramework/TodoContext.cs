using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Repositories.Models;

namespace Todo.Repositories.EntityFramework
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
