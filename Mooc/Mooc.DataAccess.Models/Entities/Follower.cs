using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Entities
{
    public class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }       //记录相关User的id

        public User User { get; set; }
    }
}
