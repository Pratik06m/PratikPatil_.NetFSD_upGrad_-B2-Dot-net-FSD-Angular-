namespace StudentGradeCalc
{
    class Student
    {
        // Method to calculate average
        public double CalculateAverage(int m1, int m2, int m3)
        {
            return (m1 + m2 + m3) / 3.0;
        }

        // Method to determine grade
        public string GetGrade(double avg)
        {
            if (avg >= 80)
                return "A";
            else if (avg >= 60)
                return "B";
            else if (avg >= 40)
                return "C";
            else
                return "Fail";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Taking input
            Console.Write("Enter marks for 3 subjects: ");
            int m1 = Convert.ToInt32(Console.ReadLine());
            int m2 = Convert.ToInt32(Console.ReadLine());
            int m3 = Convert.ToInt32(Console.ReadLine());

            // Creating object
            Student s = new Student();

            // Calling methods
            double avg = s.CalculateAverage(m1, m2, m3);
            string grade = s.GetGrade(avg);

            // Display output
            Console.WriteLine("Average = " + avg);
            Console.WriteLine("Grade = " + grade);
        }
    }
}
