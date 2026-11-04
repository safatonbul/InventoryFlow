using Microsoft.AspNetCore.Mvc;
using InventoryFlow.Business;
using InventoryFlow.Entities.Entities;
using System.Transactions;
using InventoryFlow.DTOs;
namespace InventoryFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest urundto)
        {
            if (urundto == null)
            {
                return BadRequest("Empty request.");
            }
            Product product = await _productService.CreateProduct(urundto.Name, urundto.Price, urundto.StockQuantity);
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products =await _productService.GetAllProducts();
            return Ok(products);    
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            Product? product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, int id)
        {
            if (request == null)
            {
                return BadRequest("Empty request.");
            }
            Product? product1 =await _productService.GetProductById(id);
            if (product1 == null)
            {
                return NotFound();
            }
            product1.UpdateDetails(request.Name, request.Price);
            Product updatedproduct = await _productService.UpdateProduct(product1);
            return Ok(updatedproduct);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            bool result = await _productService.DeleteProduct(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("{id}/increase-stock")]
        public async Task<IActionResult> IncreaseStock(int id ,int amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }
            Product? product=await _productService.IncreaseStock(id, amount);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("{id}/decrease-stock")]
        public async Task<IActionResult> DecreaseStock(int id, int amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }
            Product? product =await _productService.DecreaseStock(id, amount);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("transfer-stock")]
        public async Task<IActionResult> TransferStock(
        int fromProductId,
        int toProductId,
        int amount)
        {
            if (amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            try
            {
                await _productService.TransferStock(
                    fromProductId,
                    toProductId,
                    amount);

                return Ok("Stock transferred successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
