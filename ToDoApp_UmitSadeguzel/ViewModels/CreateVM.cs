using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp_UmitSadeguzel.ViewModels
{
    public class CreateVM
    {
        public int ItemId { get; set; } = 0;
        [Display(Name = "Item Description")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string ItemDesc { get; set; }
        [Display(Name = "Is Completed")]
        public bool IsCompleted { get; set; }
    }
}