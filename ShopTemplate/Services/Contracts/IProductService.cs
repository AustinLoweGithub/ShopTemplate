using ShopTemplate.Models.Dtos;

namespace ShopTemplate.Web.Services.Contracts
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetItems();

        Task<ProductDto> GetItem(int id);



       
    }
}
