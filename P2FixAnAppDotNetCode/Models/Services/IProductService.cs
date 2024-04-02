using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts(); // Change GetAllProduct return type from an Array (Product[]) to a list of product objects (List<Product>)
        Product GetProductById(int id);
        void UpdateProductQuantities(Cart cart);
    }
}
