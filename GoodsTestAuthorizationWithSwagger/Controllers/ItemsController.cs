using BLL.Repository.Users.Interfaces;
using GoodsTest.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Repository;
using BLL.Repository.Items.Interfaces;
namespace GoodsTest.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        #region Fields
        private readonly IUsersBLL _UsersBLL;
        private readonly IItemsBLL _ItemsBLL;
        #endregion
        #region Constructor
        public ItemsController(IUsersBLL userbll, IItemsBLL itemsBLL)
        {
            _UsersBLL = userbll;
            _ItemsBLL = itemsBLL;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("getitems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetItems(string strColumn, 
                                      string strValue,
                                      int PageSize,
                                      int PageNumber)
        {
            try
            {
                var items = await _ItemsBLL.GetByFilter(strColumn, strValue, PageSize, PageNumber);
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
