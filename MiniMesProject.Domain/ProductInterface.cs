public interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
}

public interface IProductRepository
{
    Task<List<IProduct>> GetAllProducts();
    Task AddProduct(IProduct product);
    Task DeleteProduct(int id);
    Task UpdateProduct(int id, IProduct updatedProductData);
}