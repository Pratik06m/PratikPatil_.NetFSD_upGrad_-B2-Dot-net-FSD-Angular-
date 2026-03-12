namespace CalCusingSwitch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double num1, num2;
            char op;

            Console.Write("Enter First Number: ");
            num1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Second Number: ");
            num2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Operator (+, -, *, /): ");
            op = Convert.ToChar(Console.ReadLine());

            switch (op)
            {
                case '+':
                    Console.WriteLine("Result: " + (num1 + num2));
                    break;

                case '-':
                    Console.WriteLine("Result: " + (num1 - num2));
                    break;

                case '*':
                    Console.WriteLine("Result: " + (num1 * num2));
                    break;

                case '/':
                    if (num2 == 0)
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                    }
                    else
                    {
                        Console.WriteLine("Result: " + (num1 / num2));
                    }
                    break;

                default:
                    Console.WriteLine("Invalid operator!");
                    break;
            }
        }
    }
}
