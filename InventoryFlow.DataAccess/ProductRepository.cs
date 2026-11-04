using Microsoft.EntityFrameworkCore;
using InventoryFlow.Business;
using InventoryFlow.DataAccess.Context;
using InventoryFlow.Entities.Entities;

namespace InventoryFlow.DataAccess;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _context;

    public ProductRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Save(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }
    public async Task<List<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Product> Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task TransferStock(int fromProductId, int toProductId, int amount)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero.");

            if (fromProductId == toProductId)
                throw new InvalidOperationException("Cannot transfer stock to the same product.");

            var fromProduct = await _context.Products.FindAsync(fromProductId);
            var toProduct = await _context.Products.FindAsync(toProductId);

            if (fromProduct == null || toProduct == null)
                throw new InvalidOperationException("Product not found.");

            fromProduct.DecreaseStock(amount);
            toProduct.IncreaseStock(amount);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
