using ShopTemplate.Api.Entities;
using ShopTemplate.Api.Data;
using Microsoft.EntityFrameworkCore;
using ShopTemplate.Api.Repositories.Contracts;

namespace ShopTemplate.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ShopTemplateDbContext shopTemplateDbContext;

        public ProductRepository(ShopTemplateDbContext shopTemplateDbContext)
        {
            this.shopTemplateDbContext = shopTemplateDbContext;

        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.shopTemplateDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await shopTemplateDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await shopTemplateDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopTemplateDbContext.Products.ToListAsync();

            return products;
        }
    }
}
