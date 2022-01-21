using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ManageViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "ID is required")]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Nama is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Nama Role")]
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Keterangan")]
        public string description { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Level")]
        public int level_id { get; set; }
    }
}
