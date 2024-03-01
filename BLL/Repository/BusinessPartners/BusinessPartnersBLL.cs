using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Repository.Items.Interfaces;
using DAL.Models;
using BLL.Repository.BusinessPartners.Interfaces;
using GoodsTest.DAL.Models;
namespace BLL.Repository.BusinessPartners
{
    public class BusinessPartnersBLL : IBusinessPartnersBLL
    {
        #region Fields
        private bool disposedValue;
        private readonly GoodsTestContext _context;
        #endregion
        #region Constructors
        public BusinessPartnersBLL(GoodsTestContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods
        public async Task<ResponseBusinessPartnersModel> GetByFilter(string strColumn, string strValue, int PageSize, int PageNumber)
        {
            bool asc = true;
            IEnumerable<BusinessPartner>? pagedlist = null;
            ResponseBusinessPartnersModel responseBusinessPartnerModel = new ResponseBusinessPartnersModel();
            IQueryable<BusinessPartner>? query = null;
            try
            {
                switch (strColumn.ToLower())
                {
                    case "bpcode":
                        query = _context.BusinessPartners.Where(u => u.Bpcode == strValue);
                        break;
                    case "bpname":
                        query = _context.BusinessPartners.Where(u => u.Bpname == strValue);
                        break;
                    case "bptype":
                        query = _context.BusinessPartners.Where(u => u.Bptype == strValue);
                        break;
                    case "active":
                        query = _context.BusinessPartners.Where(u => u.Active == Boolean.Parse(strValue));
                        break;
                    default:
                        return null;
                }
                var pageQuery = query.Page(PageNumber - 1, PageSize, p => p.Bpcode, asc);
                pagedlist = await query.ToListAsync();
                responseBusinessPartnerModel.Pagedlist = pagedlist;
                responseBusinessPartnerModel.TotalPages = query.Count() / PageSize + 1;
                return responseBusinessPartnerModel;
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
