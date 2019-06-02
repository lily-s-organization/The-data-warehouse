using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mooc.DataAccess.Models.Utils
{
    public class EnumModels
    {

        public enum RoleNameEnum
        {
            管理员 = 1,
            讲师 = 2,
            学生 = 3
        }

        public static IList<SelectListItem> ToSelectList(Type enumType)
        {
            IList<SelectListItem> listItem = new List<SelectListItem>();

            if (enumType.IsEnum)
            {
                Array values = Enum.GetValues(enumType);
                if (null != values && values.Length > 0)
                {
                    foreach (int item in values)
                    {
                        listItem.Add(new SelectListItem { Value = item.ToString(), Text = Enum.GetName(enumType, item) });
                    }
                }
            }
            else
            {
                throw new ArgumentException("请传入正确的枚举！");
            }
            return listItem;
        }

    }
}
