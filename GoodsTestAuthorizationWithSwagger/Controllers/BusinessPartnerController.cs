using BLL.Repository.Users.Interfaces;
using GoodsTest.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Repository;
using BLL.Repository.BusinessPartners.Interfaces;
namespace GoodsTest.Controllers
{
    [Route("api/businesspartner")]
    [ApiController]
    public class BusinessPartnerController : ControllerBase
    {
        #region Fields
        
        private readonly IBusinessPartnersBLL _IBusinessPartnersBLL;
        #endregion
        #region Constructor
        public BusinessPartnerController(IBusinessPartnersBLL businesspartnersBLL)
        {
            _IBusinessPartnersBLL = businesspartnersBLL;           
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("getbusinesspartners")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBusinessPartners(string strColumn,
                                      string strValue,
                                      int PageSize,
                                      int PageNumber)
        {
            try
            {
                var items = await _IBusinessPartnersBLL.GetByFilter(strColumn, strValue, PageSize, PageNumber);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
