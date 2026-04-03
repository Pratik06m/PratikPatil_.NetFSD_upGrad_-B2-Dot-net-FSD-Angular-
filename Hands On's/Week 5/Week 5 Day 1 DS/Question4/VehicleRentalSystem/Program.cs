namespace VehicleRentalSystem
{
    class Vehicle
    {
        private double rentalRatePerDay; // Encapsulation

        public string Brand { get; set; }

        public double RentalRatePerDay
        {
            get { return rentalRatePerDay; }
            set
            {
                if (value >= 0)
                    rentalRatePerDay = value;
                else
                    Console.WriteLine("Invalid rental rate!");
            }
        }

        // Virtual method
        public virtual double CalculateRental(int days)
        {
            return RentalRatePerDay * days;
        }
    }

    // Derived class - Car
    class Car : Vehicle
    {
        public override double CalculateRental(int days)
        {
            double baseCost = base.CalculateRental(days);
            return baseCost + 500; // Add insurance charge
        }
    }

    // Derived class - Bike
    class Bike : Vehicle
    {
        public override double CalculateRental(int days)
        {
            double baseCost = base.CalculateRental(days);
            double discount = baseCost * 0.05; // 5% discount
            return baseCost - discount;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter rental days: ");
            int days = Convert.ToInt32(Console.ReadLine());

            if (days <= 0)
            {
                Console.WriteLine("Invalid number of days!");
                return;
            }

            // Runtime Polymorphism
            Vehicle vehicle;

            // Car Example
            vehicle = new Car();
            vehicle.Brand = "Honda";
            vehicle.RentalRatePerDay = 2000;

            double carTotal = vehicle.CalculateRental(days);
            Console.WriteLine("Car Total Rental = " + carTotal);

            Console.WriteLine();

            // Bike Example
            vehicle = new Bike();
            vehicle.Brand = "Yamaha";
            vehicle.RentalRatePerDay = 500;

            double bikeTotal = vehicle.CalculateRental(days);
            Console.WriteLine("Bike Total Rental = " + bikeTotal);
        }
    }
}
