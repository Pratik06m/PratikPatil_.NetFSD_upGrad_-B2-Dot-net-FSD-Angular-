using System;

[cite_start]// Smaller interfaces [cite: 90, 92]
public interface IPrinter { void Print(); } // [cite: 93, 105]
public interface IScanner { void Scan(); } // [cite: 94, 106]
public interface IFax { void Fax(); } // [cite: 95, 107]

[cite_start]// Classes [cite: 96]
[cite_start]
public class BasicPrinter : IPrinter // Print only [cite: 97, 109]
{
    public void Print() => Console.WriteLine("Basic Printer is printing...");
}

[cite_start]
public class AdvancedPrinter : IPrinter, IScanner, IFax // Print + Scan + Fax [cite: 98, 110]
{
    public void Print() => Console.WriteLine("Advanced Printer is printing...");
    public void Scan() => Console.WriteLine("Advanced Printer is scanning...");
    public void Fax() => Console.WriteLine("Advanced Printer is faxing...");
}

class Program
{
    static void Main()
    {
        IPrinter basic = new BasicPrinter();
        basic.Print();

        AdvancedPrinter advanced = new AdvancedPrinter();
        advanced.Print();
        advanced.Scan();
        advanced.Fax();
    }
}