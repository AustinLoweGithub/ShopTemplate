using Microsoft.EntityFrameworkCore;
using ShopTemplate.Api.Data;
using ShopTemplate.Api.Entities;
using ShopTemplate.Api.Repositories.Contracts;
using ShopTemplate.Models.Dtos;

namespace ShopTemplate.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {


        private readonly ShopTemplateDbContext shopTemplateDbContext;

        public ShoppingCartRepository(ShopTemplateDbContext shopTemplateDbContext){


            this.shopTemplateDbContext = shopTemplateDbContext;



            }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {

            return await this.shopTemplateDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);

        }


        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {

            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {




                var item = await (from product in this.shopTemplateDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem { CartId = cartItemToAddDto.CartId, ProductId = product.Id, Qty = cartItemToAddDto.Qty }).SingleOrDefaultAsync();


                if (item != null)
                {

                    var result = await this.shopTemplateDbContext.CartItems.AddAsync(item);
                    await this.shopTemplateDbContext.SaveChangesAsync();
                    return result.Entity;

                }
            
                
            }
            return null;
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await this.shopTemplateDbContext.CartItems.FindAsync(id);

            if (item != null)
            {

                this.shopTemplateDbContext.CartItems.Remove(item);
                await this.shopTemplateDbContext.SaveChangesAsync();

            }

            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.shopTemplateDbContext.Carts
                         join cartItem in this.shopTemplateDbContext.CartItems
                         on cart.Id equals cartItem.CartId
                         where cartItem.Id == id
                          select new CartItem
                         {
                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId

                         }).SingleOrDefaultAsync();
        }
          

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in this.shopTemplateDbContext.Carts
                          join cartItem in this.shopTemplateDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId,


                          }).ToListAsync();
                          
                          
                          
                          
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this.shopTemplateDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this.shopTemplateDbContext.SaveChangesAsync();
                return item;
            }
            return null;
        }
    }
}
