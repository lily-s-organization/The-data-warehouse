using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Service
{

    public class UsersService: IUsersService
    {
        private static readonly DataContext db = new DataContext();
        public List<User> UserList()
        {
            return db.Users.ToList();
        }

        public int Add(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges();
        }

    }
}
