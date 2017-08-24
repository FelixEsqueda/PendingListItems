using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace PendingListItems.Models
{
    [Table("ListItem")]
    public class ListItemModel
    {
        [Key]
        public int ListItemId { get; set; }

        [Required]
        [Display(Name = "Item name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public int PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public virtual PriorityModel DefaultPriority { get; set; }

        public virtual ICollection<PriorityModel> Priorities { get; set; }

        [Display(Name = "Amount")]
        [Required]
        public decimal Amount { get; set; }
    }
}