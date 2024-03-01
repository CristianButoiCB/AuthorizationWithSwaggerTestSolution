using BLL.Repository.Items;
using BLL.Repository.Items.Interfaces;
using BLL.Repository.Users;
using BLL.Repository.Users.Interfaces;
using BLL.Repository.BusinessPartners;
using BLL.Repository.BusinessPartners.Interfaces;
using BLL.Repository.Documents;
using BLL.Repository.Documents.Interfaces;

namespace GoodsTest
{
  
        public static class RegisterServices
        {
            public static IServiceCollection AddUserService(this IServiceCollection services)
            {                
                return services.AddScoped<IUsersBLL, UsersBLL>();
            }
            public static IServiceCollection AddItemService(this IServiceCollection services)
            {
                return services.AddScoped<IItemsBLL, ItemsBLL>();
            }
            public static IServiceCollection AddBusinessPartnersService(this IServiceCollection services)
            {
               return services.AddScoped<IBusinessPartnersBLL, BusinessPartnersBLL>();
            }
            public static IServiceCollection AddPurchaseOrderService(this IServiceCollection services)
            {
                return services.AddScoped<IPurchaseOrderBLL, PurchaseOrderBLL>();
            }
            public static IServiceCollection AddSaleOrderService(this IServiceCollection services)
            {
                return services.AddScoped<ISaleOrderBLL, SaleOrderBLL>();
            }
    }
}
