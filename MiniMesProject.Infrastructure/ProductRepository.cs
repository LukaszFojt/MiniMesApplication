using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly MiniMesDbContext _dbContext;

    public ProductRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IProduct>> GetAllProducts()
    {
        var product = await _dbContext.Products.ToListAsync();
        return product.Cast<IProduct>().ToList();
    }


    public async Task AddProduct(IProduct product)
    {
        var concreteProduct = new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description
        };

        _dbContext.Products.Add(concreteProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProduct(int id, IProduct updatedProductData)
    {
        var product = await _dbContext.Products.FindAsync(id);

        if (product == null)
        {
            throw new ArgumentException("Product not found");
        }

        product.Name = updatedProductData.Name;
        product.Description = updatedProductData.Description;

        await _dbContext.SaveChangesAsync();
    }
}

