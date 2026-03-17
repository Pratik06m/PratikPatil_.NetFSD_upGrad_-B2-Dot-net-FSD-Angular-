namespace ResultAnalyzer
{
    class StudentResult
    {
        // Method using out parameters
        public void CalculateResult(int m1, int m2, int m3, out int total, out double average)
        {
            total = m1 + m2 + m3;
            average = total / 3.0;
        }

        // Method to validate marks
        public bool IsValid(int mark)
        {
            return mark >= 0 && mark <= 100;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            StudentResult sr = new StudentResult();

            Console.Write("Enter number of students: ");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"\n--- Student {i} ---");

                int m1, m2, m3;

                // Input with validation
                while (true)
                {
                    Console.Write("Enter marks for 3 subjects: ");
                    m1 = Convert.ToInt32(Console.ReadLine());
                    m2 = Convert.ToInt32(Console.ReadLine());
                    m3 = Convert.ToInt32(Console.ReadLine());

                    if (sr.IsValid(m1) && sr.IsValid(m2) && sr.IsValid(m3))
                        break;
                    else
                        Console.WriteLine("Invalid marks! Enter values between 0 and 100.");
                }

                // Using out parameters
                sr.CalculateResult(m1, m2, m3, out int total, out double avg);

                // Result
                string result = avg >= 40 ? "Pass" : "Fail";

                // Output
                Console.WriteLine("Total Marks = " + total);
                Console.WriteLine("Average Marks = " + avg);
                Console.WriteLine("Result = " + result);
            }
        }
    }
}
