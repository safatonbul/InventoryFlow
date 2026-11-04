using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventoryFlow.Entities.Entities
{
    public class Product
    {
        public int Id { get;private set; }
        public string Name { get; private set; }
        [Precision(18, 2)]
        public decimal Price { get; private set; } 
        public int StockQuantity { get; private set; }
        public Product(string name,decimal price , int stockQuantity) {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            } 
            Name = name;
            if(stockQuantity < 0)
            {
                throw new ArgumentOutOfRangeException("Stock quantity cannot be less than 0.");
            }
            StockQuantity = stockQuantity;
            if (price<=0)
            {
                throw new ArgumentOutOfRangeException("Price cannot be less than or equal to 0.");
            }
            Price = price;
        }
        public int IncreaseStock(int quantity)
        {
            if(quantity <= 0) {
                throw new ArgumentOutOfRangeException();
        }
            return StockQuantity+=quantity;
    }
        public int DecreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (StockQuantity<quantity)
            {
                throw new InvalidOperationException();
            }
            return StockQuantity-=quantity;
        }
        public int AddStock(Product urun,int quantity)
        {
            return urun.IncreaseStock(quantity);
            
        }
        public Product UpdateDetails(string name,decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }
            if (price<=0)
            {
                throw new ArgumentOutOfRangeException("Price cannot be less than or equal to 0.");
            }
            Price = price;
            Name = name;
            return this;
        }
        }


}
