namespace StudentGradeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            int marks;

            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Marks: ");
            marks = Convert.ToInt32(Console.ReadLine());

            if (marks < 0 || marks > 100)
            {
                Console.WriteLine("Invalid marks! Please enter marks between 0 and 100.");
            }
            else
            {
                if (marks >= 90)
                {
                    Console.WriteLine("Student: " + name + " Grade: A");
                }
                else if (marks >= 75)
                {
                    Console.WriteLine("Student: " + name + " Grade: B");
                }
                else if (marks >= 60)
                {
                    Console.WriteLine("Student: " + name + " Grade: C");
                }
                else if (marks >= 40)
                {
                    Console.WriteLine("Student: " + name + " Grade: D");
                }
                else
                {
                    Console.WriteLine("Student: " + name + " Grade: Fail");
                }
            }
        }
    }
}
