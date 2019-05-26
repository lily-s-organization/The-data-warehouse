using Mooc.DataAccess.Models.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Service
{
   public interface ITestService: IDependency
    {
         int Sum(int a, int b);
    }
}
