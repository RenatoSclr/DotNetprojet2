using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private List<CartLine> CartLines { get;} = new List<CartLine>();

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => CartLines;

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            var existingLine = CartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            
            if (existingLine != null)
            {
                if (existingLine.Product.Stock == existingLine.Quantity)
                {
                    // If cart quantity equals stock quantity, does not add product to cart
                    return;
                }

                // If the product is already in the cart, increment the quantity
                existingLine.Quantity += quantity;
                return;
            }

            //Add a new line to the cart
             CartLines.Add(new CartLine { Product = product, Quantity = quantity, OrderLineId = CartLines.Count + 1 });
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            CartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            return CartLines.Sum(cart => cart.Product.Price * cart.Quantity); 
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            if (CartLines.Count > 0)
            {
                return GetTotalValue() / CartLines.Sum(cart => cart.Quantity); 
            }
            return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            return CartLines.FirstOrDefault(p => p.Product.Id == productId)?.Product;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            CartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
