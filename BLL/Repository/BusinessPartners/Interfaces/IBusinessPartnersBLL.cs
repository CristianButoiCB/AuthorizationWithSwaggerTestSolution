using DAL.Models;
using GoodsTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository.BusinessPartners.Interfaces
{
    public interface IBusinessPartnersBLL
    {
        Task<ResponseBusinessPartnersModel> GetByFilter(string strColumn, string strValue, int PageSize, int PageNumber);
    }
}
