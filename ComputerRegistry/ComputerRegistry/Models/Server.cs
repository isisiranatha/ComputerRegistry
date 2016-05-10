using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerRegistry.Models
{
    public class Server:Computer
    {
        [MaxLength(50, ErrorMessage = "Location cannot be longer than 50 characters.")]
        public string Location { get; set; }
    }
}