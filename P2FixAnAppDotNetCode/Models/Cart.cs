using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        //New cartline list to serve as a gateway
        private List<CartLine> cartLines = new List<CartLine>();
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return cartLines; // bugfix, the method returns a new list in return
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // Implementing the method for adding a product to the cart
            var existingLine = cartLines.FirstOrDefault(l => l.Product.Id == product.Id);

            if (existingLine != null)
            {
                if (existingLine.Product.Stock == existingLine.Quantity)
                {
                    // If cart quantity equals stock quantity, does not add product to cart

                    // Maybe display a message to the user when he can no longer add to the cart ?
                    return;
                }
                else
                {
                    // If the product is already in the cart, increment the quantity
                    existingLine.Quantity += quantity;
                }
            }
            else
            {
                // If not, add a new line to the cart
                cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            return GetCartLineList().Sum(cart => cart.Product.Price * cart.Quantity); // Calculates the sum total of the prices of the items in the cart
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            if (cartLines.Count > 0)
            {
                return GetCartLineList().Average(cart => cart.Product.Price); // Calculates the average price of items in the cart
            }
            return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            return cartLines.FirstOrDefault(p => p.Product.Id == productId).Product;
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
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
