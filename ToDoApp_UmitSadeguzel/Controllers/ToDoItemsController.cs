using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoApp_UmitSadeguzel.DAL;
using ToDoApp_UmitSadeguzel.Filters;
using ToDoApp_UmitSadeguzel.Models;
using ToDoApp_UmitSadeguzel.Service;
using ToDoApp_UmitSadeguzel.ViewModels;

namespace ToDoApp_UmitSadeguzel.Controllers
{
    /*
       @author: Umit Sadeguzel
       goal: Create a to-do app. with Repository and UnitOfWork patterns
       Feature Set: 
                1- List current items.
                2- Add New Item with Ajax support
                3- Edit Item
                4- Check duplicate items
                5- Drilldown Item (Details)
    */
    public class ToDoItemsController : Controller
    {
        private UnitOfWork unitOfWork;
        public ToDoItemsController()
        {
            //this._itemRepository = new ToDoItemRepository(new ToDoItemContext());
            this.unitOfWork = new UnitOfWork();
        }
        // GET: ToDoItems
        public ActionResult Index()
        {
            ViewBag.Title = "ToDo Dashboard";
            List<ToDoItem> toDoItemList = new List<ToDoItem>();
            List<ListVM> itemList = new List<ListVM>();
            using (this.unitOfWork)
            {
                ToDoAppService s = new ToDoAppService();
                toDoItemList = s.ListAllItems();
                if (toDoItemList != null)
                {
                    foreach (var item in toDoItemList)
                    {
                        ListVM vm = new ListVM
                        {
                            ItemId = item.Id,
                            ItemDesc = item.TaskDesc,
                            IsCompleted = item.IsCompleted
                        };

                        itemList.Add(vm);
                    }
                }
               
                return View(itemList);
            }
        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int? itemId)
        {
            if (itemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var toDoItem = this.unitOfWork.ToDoItemRepository.GetByID(itemId);
            //var toDoItem = this._itemRepository.GetToDoItemByID(itemId);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        [HttpPost]
        [CheckDuplicateItems]//custom filter to check duplicate item descriptions
        public async Task<ActionResult> Create(string itemDesc)
        {
            CreateVM toDoItem = new CreateVM();
            if ( !string.IsNullOrEmpty(itemDesc) )
            {
                //this._itemRepository.InsertToDoItem(toDoItem);
                //this._itemRepository.Save();
                ToDoItem postedToDoITem = new ToDoItem();
                postedToDoITem.Id = 0;
                postedToDoITem.TaskDesc = itemDesc;
                postedToDoITem.IsCompleted = false;
                using (this.unitOfWork)
                {
                    this.unitOfWork.ToDoItemRepository.Insert(postedToDoITem);
                    await this.unitOfWork.Save();
                }   
                //return RedirectToAction("Index");

            }

            List<ToDoItem> toDoItemList = new List<ToDoItem>();
            List<ListVM> itemList = new List<ListVM>();
            using (this.unitOfWork)
            {
                ToDoAppService s = new ToDoAppService();
                toDoItemList = s.ListAllItems();
                if (toDoItemList != null)
                {
                    foreach (var item in toDoItemList)
                    {
                        ListVM vm = new ListVM
                        {
                            ItemId = item.Id,
                            ItemDesc = item.TaskDesc,
                            IsCompleted = item.IsCompleted
                        };

                        itemList.Add(vm);
                    }
                }
            }

            return PartialView("_Index", itemList);
        }

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? itemId)
        {
            if (itemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoAppService s = new ToDoAppService();
            var selectedToDoItem = s.GetSelectedItem(itemId);
            EditVM vm = new EditVM();
            if(selectedToDoItem != null)
            {
                vm.ItemId = selectedToDoItem.Id;
                vm.ItemDesc = selectedToDoItem.TaskDesc;
                vm.IsCompleted = selectedToDoItem.IsCompleted;
            }
            
            if (selectedToDoItem == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: ToDoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckDuplicateItems]
        public async Task<ActionResult> Edit(EditVM postedItem)
        {
            if (ModelState.IsValid)
            {
                //this._itemRepository.UpdateToDoItem(toDoItem);
                //this._itemRepository.Save();
                ToDoItem editItem = new ToDoItem();
                editItem.Id = postedItem.ItemId;
                editItem.TaskDesc = postedItem.ItemDesc;
                editItem.IsCompleted = postedItem.IsCompleted;
                using (this.unitOfWork)
                {
                    this.unitOfWork.ToDoItemRepository.Update(editItem);
                    await this.unitOfWork.Save();
                }
                return RedirectToAction("Index");
            }
            return View(postedItem);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
