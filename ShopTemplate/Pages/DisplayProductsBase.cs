using Microsoft.AspNetCore.Components;
using ShopTemplate.Models.Dtos;

namespace ShopTemplate.Web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
