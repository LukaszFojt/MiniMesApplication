using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        return Ok(products);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        await _productRepository.AddProduct(product);
        return Ok("Rekord " + product + " został dodany.");
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        await _productRepository.DeleteProduct(id);
        return Ok("Product with ID " + id + " has been deleted.");
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProductData)
    {
        await _productRepository.UpdateProduct(id, updatedProductData);
        return Ok("Product with ID " + id + " has been updated.");
    }
}
