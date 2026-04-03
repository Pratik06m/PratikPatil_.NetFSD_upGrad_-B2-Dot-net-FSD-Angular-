namespace BankAccEncapusulation
{
    class BankAccount
    {
        // Private field (Encapsulation)
        private double balance;

        // Method to deposit money
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine("Deposited: " + amount);
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }
        // Method to withdraw money
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
            }
            else if (amount > balance)
            {
                Console.WriteLine("Insufficient balance!");
            }
            else
            {
                balance -= amount;
                Console.WriteLine("Withdrawn: " + amount);
            }
        }

        // Method to get balance
        public double GetBalance()
        {
            return balance;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();

            // Sample operations
            account.Deposit(7368);
            account.Withdraw(300);

            // Display balance
            Console.WriteLine("Current Balance = " + account.GetBalance());
        }
    }
}
