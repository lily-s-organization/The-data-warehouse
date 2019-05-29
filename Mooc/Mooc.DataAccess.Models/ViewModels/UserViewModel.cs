using Mooc.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.ViewModels
{
    public class UserViewModel:User
    {

        [Required(ErrorMessage = "Please re-type your password")]
        [Compare("PassWord", ErrorMessage = "password and re-type password must match")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
    }
}
