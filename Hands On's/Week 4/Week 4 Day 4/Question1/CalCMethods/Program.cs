namespace CalCMethods
{
    class Calculator
    {
        // Method for Addition
        public int Add(int a, int b)
        {
            return a + b;
        }

        // Method for Subtraction
        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Taking input
            Console.Write("Enter first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            // Creating object
            Calculator calc = new Calculator();

            // Calling methods
            int addition = calc.Add(num1, num2);
            int subtraction = calc.Subtract(num1, num2);

            // Display output
            Console.WriteLine("Addition = " + addition);
            Console.WriteLine("Subtraction = " + subtraction);
        }
    }
}
