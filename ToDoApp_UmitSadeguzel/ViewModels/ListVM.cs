using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp_UmitSadeguzel.ViewModels
{
    public class ListVM
    {
        public int ItemId { get; set; }
        [Display(Name = "Item")]
        public string ItemDesc { get; set; }
        [Display(Name = "Status")]
        public bool IsCompleted { get; set; }
    }
}