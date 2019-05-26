using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Common.Utils
{
    public static class ObjectToInt
    {

        public static int ConvertInt(this object obj,int res=0)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception e)
            {
                return res;
            }
        }

    }
}
