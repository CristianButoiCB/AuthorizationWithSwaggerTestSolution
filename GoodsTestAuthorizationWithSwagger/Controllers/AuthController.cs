using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoodsTest.DAL.Models;
using BLL;
using BLL.Repository.Users.Interfaces;
using BLL.Repository.Users;

namespace GoodsTest.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly IUsersBLL _UsersBLL;
        #endregion
        #region Constructor
        public AuthController(IUsersBLL userbll)
        {
            _UsersBLL = userbll;
        }
        #endregion
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }
            var user =await _UsersBLL.GetByUserIdAndPassword(model.UserName, model.Password);
            if (user!=null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@2410"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claimsdata = new[] {
                    new Claim("UserId", user.Id.ToString()) };
                var tokenOptions = new JwtSecurityToken(
                    issuer: "GoodsTest",
                    audience: "https://localhost:5001",
                    claims: claimsdata,
                    expires: DateTime.Now.AddMinutes(50),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
