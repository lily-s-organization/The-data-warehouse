using Mooc.DataAccess.Models.App_Start;
using Mooc.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Service
{
    public interface IUsersService : IDependency
    {
        List<User> UserList();
        int Add(User user);
    }
}
