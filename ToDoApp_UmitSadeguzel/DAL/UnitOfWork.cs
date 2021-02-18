using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ToDoApp_UmitSadeguzel.Models;

namespace ToDoApp_UmitSadeguzel.DAL
{
    public class UnitOfWork : IDisposable
    {
        //Let all repositories share the same context instance.
        private ToDoItemContext context = new ToDoItemContext();
        private GenericRepository<ToDoItem> toDoItemRepository;

        public GenericRepository<ToDoItem> ToDoItemRepository
        {
            get
            {

                if (this.toDoItemRepository == null)
                {
                    this.toDoItemRepository = new GenericRepository<ToDoItem>(context);
                }
                return toDoItemRepository;
            }
        }
        
        public async Task Save()
        {
           await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}