
using DAL.Models;
using GoodsTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository.Items.Interfaces
{
    public interface IItemsBLL
    {
        public Task<ResponseItemsModel> GetByFilter(string strColumn, string strValue, int PageSize, int PageNumber);
    }
}
