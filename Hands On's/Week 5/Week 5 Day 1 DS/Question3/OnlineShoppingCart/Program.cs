namespace OnlineShoppingCart
{
    class Product
    {
        private double price; // Encapsulation

        public string Name { get; set; }

        // Property with validation
        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                    price = value;
                else
                    Console.WriteLine("Price cannot be negative!");
            }
        }

        // Virtual method
        public virtual double CalculateDiscount()
        {
            return 0;
        }

        // Method to calculate final price
        public double GetFinalPrice()
        {
            double discount = CalculateDiscount();
            return Price - discount;
        }
    }

    // Derived class - Electronics
    class Electronics : Product
    {
        public override double CalculateDiscount()
        {
            return Price * 0.05; // 5% discount
        }
    }

    // Derived class - Clothing
    class Clothing : Product
    {
        public override double CalculateDiscount()
        {
            return Price * 0.15; // 15% discount
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Product product;

            // Electronics Example
            product = new Electronics();
            product.Name = "Laptop";
            product.Price = 20000;

            Console.WriteLine("Electronics Product: " + product.Name);
            Console.WriteLine("Original Price = " + product.Price);
            Console.WriteLine("Final Price after 5% discount = " + product.GetFinalPrice());

            Console.WriteLine();

            // Clothing Example
            product = new Clothing();
            product.Name = "Shirt";
            product.Price = 2000;

            Console.WriteLine("Clothing Product: " + product.Name);
            Console.WriteLine("Original Price = " + product.Price);
            Console.WriteLine("Final Price after 15% discount = " + product.GetFinalPrice());
        }
    }
}
