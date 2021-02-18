using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoApp_UmitSadeguzel.Models;

namespace ToDoApp_UmitSadeguzel.DAL
{
    public interface IToDoItemRepository : IDisposable
    {
        IEnumerable<ToDoItem> GetToDoItems();
        ToDoItem GetToDoItemByID(int? itemId);
        void InsertToDoItem(ToDoItem item);        
        void UpdateToDoItem(ToDoItem item);
        void Save();
    }
}