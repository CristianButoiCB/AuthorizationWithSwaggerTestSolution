using System.ComponentModel.DataAnnotations;
//in order to scaffold the db we will need to run this  dotnet ef dbcontext scaffold "Server=cristilaptop11\CRISTILAPTOP1112;Database=GoodsTest;Trusted_Connection=no;user=sn;password=SN1" Microsoft.EntityFrameworkCore.SqlServer -o Models
namespace GoodsTest.DAL.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Email Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
