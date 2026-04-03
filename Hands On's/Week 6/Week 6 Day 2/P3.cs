using System;

[cite_start]// Base class or interface [cite: 63]
[cite_start]
public abstract class Shape // [cite: 64]
{
    public abstract double CalculateArea(); // [cite: 68, 69]
}

[cite_start]// Derived classes [cite: 65]
[cite_start]
public class Rectangle : Shape // [cite: 66]
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double CalculateArea() => Width * Height;
}

[cite_start]
public class Circle : Shape // [cite: 67]
{
    public double Radius { get; set; }

    public override double CalculateArea() => Math.PI * Radius * Radius;
}

class Program
{
    [cite_start]// A method should accept Shape object and calculate area [cite: 70]
    static void PrintArea(Shape shape)
    {
        Console.WriteLine($"Area: {shape.CalculateArea():F2}");
    }

    static void Main()
    {
        Shape myRectangle = new Rectangle { Width = 5, Height = 10 }; // [cite: 76]
        Shape myCircle = new Circle { Radius = 7 }; // [cite: 77]

        PrintArea(myRectangle);
        PrintArea(myCircle);
    }
}