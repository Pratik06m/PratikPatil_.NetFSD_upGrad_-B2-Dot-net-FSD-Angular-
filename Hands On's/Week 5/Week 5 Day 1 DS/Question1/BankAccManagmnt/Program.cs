namespace BankAccManagmnt
{
    class BankAccount
    {
        // Private fields
        private string accountNumber;
        private double balance;

        // Property for Account Number
        public string AccountNumber
        {
            get { return accountNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    accountNumber = value;
                else
                    Console.WriteLine("Invalid Account Number.");
            }
        }

        // Property for Balance (Read-only outside)
        public double Balance
        {
            get { return balance; }
        }

        // Deposit method
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }

            balance += amount;
            Console.WriteLine("Deposited: " + amount);
            Console.WriteLine("Current Balance = " + balance);
        }

        // Withdraw method
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return;
            }

            if (amount > balance)
            {
                Console.WriteLine("Insufficient balance!");
                return;
            }

            balance -= amount;
            Console.WriteLine("Withdrawn: " + amount);
            Console.WriteLine("Current Balance = " + balance);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();

            // Setting account number using property
            account.AccountNumber = "ACC123";

            // Sample operations
            account.Deposit(5000);
            account.Withdraw(2000);

            // Final balance display
            Console.WriteLine("\nFinal Balance = " + account.Balance);
        }
    }
}
