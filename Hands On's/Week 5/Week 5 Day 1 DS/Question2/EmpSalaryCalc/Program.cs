namespace EmpSalaryCalc
{
    class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }

        // Virtual method
        public virtual double CalculateSalary()
        {
            return BaseSalary;
        }
    }

    // Derived class - Manager
    class Manager : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.20); // 20% bonus
        }
    }

    // Derived class - Developer
    class Developer : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.10); // 10% bonus
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Base Salary: ");
            double baseSalary = Convert.ToDouble(Console.ReadLine());

            // Runtime Polymorphism (base class reference)
            Employee emp;

            // Manager object
            emp = new Manager { Name = "Manager", BaseSalary = baseSalary };
            Console.WriteLine("Manager Salary = " + emp.CalculateSalary());

            // Developer object
            emp = new Developer { Name = "Developer", BaseSalary = baseSalary };
            Console.WriteLine("Developer Salary = " + emp.CalculateSalary());
        }
    }
}
