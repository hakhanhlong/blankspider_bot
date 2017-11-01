using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api.Interfaces
{
    public interface IProjectImpl
    {
        Task<string> GetAll();
        Task<string> GetByID(string id);

        Task<string> GetByServerIP(string serverip);

        T GetAll<T>();

        T GetByServerIP<T>(string serverip);
    }
}
