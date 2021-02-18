using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoApp_UmitSadeguzel.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string TaskDesc { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}