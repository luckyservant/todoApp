using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoApp_UmitSadeguzel.DAL;
using ToDoApp_UmitSadeguzel.Models;

namespace ToDoApp_UmitSadeguzel.Service
{
    public class ToDoAppService
    {
        private UnitOfWork unitOfWork;
        public ToDoAppService()
        {
            //this._itemRepository = new ToDoItemRepository(new ToDoItemContext());
            this.unitOfWork = new UnitOfWork();
        }

        public List<ToDoItem> ListAllItems()
        {
            var toDoItemList = this.unitOfWork.ToDoItemRepository.Get().ToList();
            return toDoItemList;
        }

        public ToDoItem GetSelectedItem(int? itemId)
        {
            var selectedToDoItem = this.unitOfWork.ToDoItemRepository.GetByID(itemId);
            return selectedToDoItem;
        }





        public bool CheckIfDuplicateItemDesc(int postedItemd,string postedDesc) {
            bool result = false;
            var repoItems = this.unitOfWork.ToDoItemRepository.Get();
            if (repoItems != null)
            {
                var dbItem = repoItems.FirstOrDefault(i => i.TaskDesc == postedDesc);
                //any existing item but itself
                if (dbItem != null && dbItem.Id != postedItemd)
                {
                    result = true;
                }
            }
            
            return result;
        }
    }
}