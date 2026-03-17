namespace ECommerceOrderCalc
{
    class OrderCalculator
    {
        // Method with optional parameters
        public double CalculateFinalAmount(int price, int quantity, double discountPercent = 0, double shippingCharge = 50)
        {
            double subtotal = price * quantity;

            double discountAmount = (subtotal * discountPercent) / 100;

            double finalAmount = subtotal - discountAmount + shippingCharge;

            // Display breakdown
            Console.WriteLine("Subtotal = " + subtotal);
            Console.WriteLine("Discount = " + discountAmount);
            Console.WriteLine("Shipping Charge = " + shippingCharge);

            return finalAmount;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderCalculator oc = new OrderCalculator();

            Console.Write("Enter product price: ");
            int price = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n--- Case 1: No Discount, Default Shipping ---");
            double result1 = oc.CalculateFinalAmount(price, quantity);
            Console.WriteLine("Final Amount = " + result1);

            Console.WriteLine("\n--- Case 2: With Discount (10%) ---");
            double result2 = oc.CalculateFinalAmount(price, quantity, 10);
            Console.WriteLine("Final Amount = " + result2);

            Console.WriteLine("\n--- Case 3: With Discount (10%) and Custom Shipping (100) ---");
            double result3 = oc.CalculateFinalAmount(price, quantity, 10, 100);
            Console.WriteLine("Final Amount = " + result3);
        }
    }
}
