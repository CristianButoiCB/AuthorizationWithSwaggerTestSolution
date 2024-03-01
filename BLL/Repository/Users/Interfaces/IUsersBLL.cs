using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository.Users.Interfaces
{
    public interface IUsersBLL : IDisposable
    {
        Task<User> GetByUserIdAndPassword(string strUserName, string strPassword);
    }
}
