// See https://aka.ms/new-console-template for more information
using InventoryFlow.Entities.Entities;
using Microsoft.EntityFrameworkCore;
namespace InventoryFlow.Business;
public class ProductService
{
    public async Task<Product> CreateProduct(string name, decimal price, int StockQuantity)
    {
        Product urun = new Product(name, price, StockQuantity);
        await productRepository.Save(urun);
        return urun;
    }
    private readonly IProductRepository productRepository;
    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    public async Task<List<Product>>GetAllProducts()
    {
        return await productRepository.GetAll();
    }
    public async Task<Product?> GetProductById(int id)
    {
        return await productRepository.GetById(id);
    }
    public async Task<Product> UpdateProduct(Product product)
    {
        return await productRepository.Update(product);
    }
    public async Task<bool> DeleteProduct(int id)
    {
        return await productRepository.Delete(id);
    }
    public async Task<Product?>IncreaseStock(int productId, int amount)
    {
        Product? product =await  productRepository.GetById(productId);
        if (product == null)
        {
            return null;
        }
        product.IncreaseStock(amount);
        return await UpdateProduct(product);

    }
    public async Task<Product?> DecreaseStock(int productId, int amount)
    {
        Product? product =await productRepository.GetById(productId);
        if (product == null)
        {
           return null;
        }
        try
        {
            product.DecreaseStock(amount);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw;
        }
        return await UpdateProduct(product);
    }
    public async Task TransferStock(int fromProductId, int toProductId, int amount)
    {
        await productRepository.TransferStock(fromProductId, toProductId, amount);
    }
}