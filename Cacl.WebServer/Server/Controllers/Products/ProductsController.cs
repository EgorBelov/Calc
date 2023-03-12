using CaclApi.Server.Controllers.Products.Services;
using CaclApi.Server.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CaclApi.Server.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ProductDto> Get([FromQuery] int id, CancellationToken ct)
        {
            return await _productsService.Get(id, ct);
        }
    }
}
