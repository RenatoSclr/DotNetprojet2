using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts(); // Change GetAllProduct return type from an Array (Product[]) to a list of product objects (List<Product>)

        void UpdateProductStocks(int productId, int quantityToRemove);
    }
}
