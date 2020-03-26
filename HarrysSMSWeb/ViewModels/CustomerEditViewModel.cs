using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HarrysSMSWeb.ViewModels
{
    public class CustomerEditViewModel
    {
        public int Id { get; set; }
        
        [DisplayName("Personnumer")]
        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string PersonNummer { get; set; }
        [DisplayName("Namn")]
        [Required]
        [MinLength(3)]
        [MaxLength(55)]
        public string Namn { get; set; }
    }
}
