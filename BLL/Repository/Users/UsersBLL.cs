using BLL.Repository.Users.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository.Users
{
    public class UsersBLL : IUsersBLL
    {
        #region Fields
        private bool disposedValue;
        private readonly GoodsTestContext _context;
        #endregion
        #region Constructors
        public UsersBLL(GoodsTestContext context)
        {
            _context = context;            
        }
        #endregion


        #region Methods
        public async Task<User> GetByUserIdAndPassword(string strUserName, string strPassword)
        {
            try
            {
                var query = _context.Users.Where(u => u.UserName == strUserName && u.Password == strPassword && (u.Active.HasValue && u.Active.Value));
                return await query.FirstOrDefaultAsync<User>();
            }
            catch (Exception ex)
            {
                throw (new blException(ex.Message));
            }
        }
        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CompanyBLL()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
