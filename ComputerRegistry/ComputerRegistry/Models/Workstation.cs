using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerRegistry.Models
{
    public class Workstation:Computer
    {

        [Display(Name = "Primary User")]
        [MaxLength(50, ErrorMessage = "Primary User name cannot be longer than 50 characters.")]
        public string PrimaryUser { get; set; }

    }
}