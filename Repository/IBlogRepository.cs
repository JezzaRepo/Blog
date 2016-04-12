using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBlogRepository
    {
        bool DeleteAllUsers();
        long GetUserCount();
        System.Collections.Generic.List<string> GetUserNames();
        string InsertUser(string email, string password);
        bool LoginUser(string username, string password);
    }
}
