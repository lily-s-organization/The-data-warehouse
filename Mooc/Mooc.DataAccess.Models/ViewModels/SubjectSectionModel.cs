using Mooc.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.ViewModels
{
   public class SubjectSectionModel
    {
        public SubjectSectionModel()
        {
            VideoList = new List<Video>();
        }
        public string sectionName { get; set; }

        public List<Video> VideoList { get; set; }
    }
}
