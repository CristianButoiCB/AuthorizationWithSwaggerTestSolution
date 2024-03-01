using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Repository.Items.Interfaces;
using GoodsTest.DAL.Models;

namespace BLL.Repository.Items
{
    public class ItemsBLL:IItemsBLL
    {
        #region Fields
        private bool disposedValue;
        private readonly GoodsTestContext _context;
        #endregion
        #region Constructors
        public ItemsBLL(GoodsTestContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods
        public async Task<ResponseItemsModel> GetByFilter(string strColumn, string strValue, int PageSize, int PageNumber)
        {
            bool asc = true;
            IEnumerable<Item>? pagedlist = null;
            ResponseItemsModel responseItemsModel = new ResponseItemsModel();
            IQueryable<Item>? query =null;
            try { 
                switch (strColumn.ToLower())
                {
                    case "itemcode":
                        query = _context.Items.Where(u => u.ItemCode == strValue);  
                        break;
                    case "itemname":
                        query = _context.Items.Where(u => u.ItemName == strValue);                    
                        break;
                    case "active":
                        query = _context.Items.Where(u => u.Active == Boolean.Parse(strValue));                         
                        break;
                    default:
                        return null;
                }
                var pageQuery = query.Page(PageNumber - 1, PageSize, p => p.ItemCode, asc);
                pagedlist = await query.ToListAsync();
                responseItemsModel.Pagedlist = pagedlist;
                responseItemsModel.TotalPages = query.Count() / PageSize + 1;
                return responseItemsModel;
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
