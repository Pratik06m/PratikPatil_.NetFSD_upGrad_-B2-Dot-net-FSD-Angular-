using System;

[cite_start]// Interface [cite: 163]
[cite_start]
public interface INotification // [cite: 164, 171]
{
    void Send(string message); // [cite: 165, 166]
}

[cite_start]// Classes implementing INotification [cite: 167, 171]
[cite_start]
public class EmailNotification : INotification // [cite: 168]
{
    public void Send(string message) => Console.WriteLine($"Sending Email: {message}");
}

[cite_start]
public class SMSNotification : INotification // [cite: 169]
{
    public void Send(string message) => Console.WriteLine($"Sending SMS: {message}");
}

[cite_start]
public class PushNotification : INotification // [cite: 170]
{
    public void Send(string message) => Console.WriteLine($"Sending Push Notification: {message}");
}

[cite_start]// Factory Class [cite: 172]
[cite_start]
public class NotificationFactory // [cite: 173]
{
    [cite_start]
    public INotification CreateNotification(string type) // [cite: 174, 175]
    {
        return type.ToLower() switch
        {
            "email" => new EmailNotification(),
            "sms" => new SMSNotification(),
            "push" => new PushNotification(),
            _ => throw new ArgumentException("Invalid notification type")
        };
    }
}

class Program
{
    static void Main()
    {
        [cite_start]// Students should demonstrate: [cite: 184, 185]
        NotificationFactory factory = new NotificationFactory();

        var notification = factory.CreateNotification("email");
        notification.Send("Welcome to our service!");

        var sms = factory.CreateNotification("sms");
        sms.Send("Your OTP is 1234");
    }
}