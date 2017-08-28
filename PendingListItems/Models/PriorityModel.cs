using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PendingListItems.Models
{
    [Table("Priority")]
    public class PriorityModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PriorityId { get; set; }
        
        [Display(Name = "Priority")]
        public string Description { get; set; }
    }
}