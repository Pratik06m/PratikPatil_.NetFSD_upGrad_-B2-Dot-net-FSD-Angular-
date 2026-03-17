namespace RecursivePowerCalc
{
    class PowerCalculator
    {
        // Recursive method
        public int CalculatePower(int baseNum, int exponent)
        {
            // Base case
            if (exponent == 0)
                return 1;

            // Recursive call
            return baseNum * CalculatePower(baseNum, exponent - 1);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            PowerCalculator pc = new PowerCalculator();

            Console.Write("Enter base: ");
            int baseNum = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter exponent: ");
            int exponent = Convert.ToInt32(Console.ReadLine());

            // Validation (positive exponent only)
            if (exponent < 0)
            {
                Console.WriteLine("Exponent must be a positive integer.");
                return;
            }

            int result = pc.CalculatePower(baseNum, exponent);

            Console.WriteLine($"Result: {baseNum}^{exponent} = {result}");
        }
    }
}
