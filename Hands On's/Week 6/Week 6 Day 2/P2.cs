using System;

[cite_start]// Abstract class or interface [cite: 35]
[cite_start]
public interface IDiscountStrategy // [cite: 36, 49]
{
    double CalculateDiscount(double amount); // [cite: 42]
}

[cite_start]// Discount classes [cite: 37]
[cite_start]
public class RegularCustomerDiscount : IDiscountStrategy // [cite: 38]
{
    public double CalculateDiscount(double amount) => amount * 0.05; // 5% discount
}

[cite_start]
public class PremiumCustomerDiscount : IDiscountStrategy // [cite: 39]
{
    public double CalculateDiscount(double amount) => amount * 0.10; // 10% discount
}

[cite_start]
public class VipCustomerDiscount : IDiscountStrategy // [cite: 40]
{
    public double CalculateDiscount(double amount) => amount * 0.20; // 20% discount
}

[cite_start]// Class that calculates the final price [cite: 51]
public class PriceCalculator
{
    public double GetFinalPrice(double amount, IDiscountStrategy discountStrategy)
    {
        double discount = discountStrategy.CalculateDiscount(amount);
        return amount - discount;
    }
}

class Program
{
    static void Main()
    {
        double amount = 1000;
        var calculator = new PriceCalculator();

        Console.WriteLine($"Regular Final Price: {calculator.GetFinalPrice(amount, new RegularCustomerDiscount())}");
        Console.WriteLine($"Premium Final Price: {calculator.GetFinalPrice(amount, new PremiumCustomerDiscount())}");
        Console.WriteLine($"VIP Final Price: {calculator.GetFinalPrice(amount, new VipCustomerDiscount())}");
    }
}