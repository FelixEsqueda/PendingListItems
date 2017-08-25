using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PendingListItems.Models
{
    [Table("Concept")]
    public class ConceptModel
    {
        [Key]
        public int ConceptId { get; set; }
        public string ConceptName { get; set; }
        public decimal Amount { get; set; }
        public bool Payd { get; set; }
        public decimal Prepayment { get; set; }
        public decimal? AmountPayable { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

        public int SummaryId { get; set; }

        [ForeignKey("SummaryId")]
        public SummaryModel Summary { get; set; }
    }
}