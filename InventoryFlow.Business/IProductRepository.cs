using InventoryFlow.Entities.Entities;
using InventoryFlow.Business; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryFlow.Business
{
        public interface IProductRepository
        {
            public Task<Product> Save(Product product);
            public Task<List<Product>> GetAll();
            public Task<Product?> GetById(int id);
            public Task<Product> Update(Product product);
            public Task<bool> Delete(int id);
            public Task TransferStock(int fromProductId, int toProductId, int amount);
    }
}
