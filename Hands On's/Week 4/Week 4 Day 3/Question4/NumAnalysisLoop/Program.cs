namespace NumAnalysisLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N;
            int evenCount = 0;
            int oddCount = 0;
            int sum = 0;

            Console.Write("Enter Number: ");
            N = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= N; i++)
            {
                sum = sum + i;

                if (i % 2 == 0)
                {
                    evenCount++;
                }
                else
                {
                    oddCount++;
                }
            }

            Console.WriteLine("Even Count: " + evenCount);
            Console.WriteLine("Odd Count: " + oddCount);
            Console.WriteLine("Sum: " + sum);
        }
    }
}
