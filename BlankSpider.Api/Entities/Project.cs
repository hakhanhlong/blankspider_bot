using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api.Entities
{
    public class Project
    {
        public string id { get; set; }
        public string created_by { get; set; }
        public string name { get; set; }
        public int source_count { get; set; }

        public List<Source> Sources { get; set; }

    }

    public class ListProjects
    {
        public List<Project> Projects { get; set; }
    }
}
