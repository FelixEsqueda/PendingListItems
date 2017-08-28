using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PendingListItems.Models
{
    [Table("Summary")]
    public class SummaryModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SummaryId { get; set; }
        public string SummaryName { get; set; }
        public decimal? UpdatedAmount { get; set; }
        public decimal? PendingAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        [Required]
        [Display(Name = "Period")]
        public int PeriodId { get; set; }
        [ForeignKey("PeriodId")]
        public virtual PeriodModel DefaultPeriod { get; set; }

        public virtual ICollection<PeriodModel> Periods { get; set; }
    }
}