using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoApp_UmitSadeguzel.Models;

namespace ToDoApp_UmitSadeguzel.DAL
{
    public class ToDoItemRepository : IToDoItemRepository, IDisposable
    {
        private ToDoItemContext _context;
        public ToDoItemRepository(ToDoItemContext itemContext)
        {
            this._context = itemContext;
        }
        public IEnumerable<ToDoItem> GetToDoItems()
        {
            return _context.ToDoItems.ToList();
        }
        public ToDoItem GetToDoItemByID(int? itemId)
        {
            return _context.ToDoItems.Find(itemId);
        }
        public void InsertToDoItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
        }
        
        public void UpdateToDoItem(ToDoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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