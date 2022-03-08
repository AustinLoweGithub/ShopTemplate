using ShopTemplate.Models.Dtos;

namespace ShopTemplate.web.Services.Contracts
{ 
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);

        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);


        Task<CartItemDto> DeleteItem(int id);

    }
}
