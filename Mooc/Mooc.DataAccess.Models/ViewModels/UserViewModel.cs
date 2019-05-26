using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public int UserState { get; set; }
        public int RoleType { get; set; }
    }
}
