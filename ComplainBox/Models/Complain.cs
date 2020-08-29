using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainBox.Models
{
    public class Complain
    {
        [Key]
        public int ComplainId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Complain Title")]
        public string ComplainTitle { get; set; }

        [Display(Name = "Description")]
        public string ComplainDescription { get; set; }

        public string Status { get; set; }

        [Display(Name = "Assigner Name")]
        public int UserId { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
