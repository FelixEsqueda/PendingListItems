using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PendingListItems.Models
{
    [Table("Period")]
    public class PeriodModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PeriodId { get; set; }

        [Display(Name = "Period")]
        public string PeriodName { get; set; }
    }
}