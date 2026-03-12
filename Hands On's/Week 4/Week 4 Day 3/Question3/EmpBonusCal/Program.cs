namespace EmpBonusCal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            double salary, bonus, finalSalary;
            int experience;
            double bonusPercentage;

            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            salary = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Experience (years): ");
            experience = Convert.ToInt32(Console.ReadLine());

            if (experience < 2)
            {
                bonusPercentage = 0.05;
            }
            else if (experience >= 2 && experience <= 5)
            {
                bonusPercentage = 0.10;
            }
            else
            {
                bonusPercentage = 0.15;
            }

            bonus = salary > 0 ? salary * bonusPercentage : 0;

            finalSalary = salary + bonus;

            Console.WriteLine("\nEmployee: " + name);
            Console.WriteLine("Bonus: " + bonus.ToString("C"));
            Console.WriteLine("Final Salary: " + finalSalary.ToString("C"));
        }
    }
}
