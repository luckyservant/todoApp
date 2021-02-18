using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoApp_UmitSadeguzel.Models;

namespace ToDoApp_UmitSadeguzel.DAL
{
    public class ToDoItemContext : DbContext
    {
        public ToDoItemContext(): base("name=ToDoListDBContext")
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}