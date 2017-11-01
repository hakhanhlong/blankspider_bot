using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api.Interfaces
{
    public interface ISourceImpl
    {
        Task<string> GetAll();

        Task<string> GetByServerIP(string serverip);

        Task<string> GetByID(string id);

        Task<string> GetConfigGeneral(string id);
        Task<string> GetConfigLink(string id);

        Task<string> GetConfigField(string id);
    }
}
