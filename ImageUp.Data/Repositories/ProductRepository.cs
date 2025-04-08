using ImageUp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUp.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductAsync();
        Task<Product?> FindProductByIdAsync(int id);
        Task DeleteProductAsync(Product product);
    }

    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        public async Task<Product> AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            
        }

        public async Task<Product?> FindProductByIdAsync(int id)
        {
            var product = await  context.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
