using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainBox.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Display(Name = "Appointment Title")]
        public string AppointmentTitle { get; set; }

        [Display(Name = "Description")]
        public string AppointmentDescription { get; set; }

        [Display(Name = "Appointment date")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        public DateTime? AppointmentDate { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        //[Display(Name = "Coordinator")]
        //public int CoordinatorId { get; set; }

        //public virtual UserAccount UserAccounts { get; set; }
    }
}
