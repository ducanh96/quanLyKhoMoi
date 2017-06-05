using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models.ViewModel
{
    public class User
    {
        [Required(ErrorMessage ="Không được để trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }
        public int Role { get; set; }


    }
}