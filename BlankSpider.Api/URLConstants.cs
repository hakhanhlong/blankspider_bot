using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api
{
    public class URLConstants
    {
        #region URL Sources

        public const string SOURCE_GET_ALL = "sources";
        public const string SOURCE_GETBY_ID = "sources/{0}";
        public const string SOURCE_GETBY_SERVERIP = "sources/server/{0}";
        public const string SOURCE_GET_CONFIG_GENERALS = "sources/config/generals/{0}";
        public const string SOURCE_GET_CONFIG_LINKS = "sources/config/parserlinks/{0}";
        public const string SOURCE_GET_CONFIG_FIELDS = "sources/config/parserfields/{0}";
        public const string SOURCE_GET_CONFIG_VIDEOS = "sources/config/parservideos/{0}";
        #endregion

        #region URL Projects
        public const string PROJECT_GET_ALL = "projects";
        public const string PROJECT_GETBY_ID = "projects/{0}";
        public const string PROJECT_GETBY_SERVERIP = "projects/server/{0}";
        #endregion
    }
}
