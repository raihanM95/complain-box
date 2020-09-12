using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainBox.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string NoteDescription { get; set; }

        public int UserId { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
