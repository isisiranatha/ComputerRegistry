using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerRegistry.Models
{
    public class ServerComputer
    {
        [Key]
        public int ServerComputerID { get; set; }

        [Display(Name = "Internal Identifier")]
        [Required(ErrorMessage = "Internal Identifier is required.")]
        public int InternalID { get; set; }

        [Display(Name = "DNS Name")]
        [Required(ErrorMessage = "DNS Name required.")]
        [MaxLength(50, ErrorMessage = "DNS Name cannot be longer than 50 characters.")]
        public string DNSName { get; set; }

        [Display(Name = "Make")]
        public int MakeID { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [MaxLength(50, ErrorMessage = "Model cannot be longer than 50 characters.")]
        public string Model { get; set; }

        [MaxLength(50, ErrorMessage = "Location cannot be longer than 50 characters.")]
        public string Location { get; set; }

        [Display(Name = "Purchased On")]
        [Required(ErrorMessage = "Date Purchased is required.")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime PurchasedOn { get; set; }

        public bool Retire { get; set; }

        public virtual Make Make { get; set; }
    }
}