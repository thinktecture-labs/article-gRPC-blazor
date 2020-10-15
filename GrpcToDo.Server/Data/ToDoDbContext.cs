using GrpcToDo.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GrpcToDo.Server.Data
{
    public class ToDoDbContext: DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) 
            : base(options)
        {

        }

        public DbSet<ToDoData> ToDoDbItems { get; set; }
    }
}
