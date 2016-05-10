using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerRegistry.Models
{
    public class Make
    {
        [Key]
        public int MakeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}