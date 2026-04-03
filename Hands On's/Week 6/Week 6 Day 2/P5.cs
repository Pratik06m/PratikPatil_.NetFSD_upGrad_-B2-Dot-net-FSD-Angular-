using System;

[cite_start]
public class ConfigurationManager // [cite: 125]
{
    [cite_start]// Static instance variable [cite: 137]
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object(); // For thread safety 

    [cite_start]// Store configuration values [cite: 130]
    public string ApplicationName { get; private set; } // [cite: 131]
    public string Version { get; private set; } // [cite: 132]
    public string DatabaseConnectionString { get; private set; } // [cite: 133]

    [cite_start]// Private constructor [cite: 136]
    private ConfigurationManager()
    {
        ApplicationName = "InventorySystem";
        Version = "1.0.0";
        DatabaseConnectionString = "Server=myServer;Database=myDB;Trusted_Connection=True;";
    }

    [cite_start]// Method to retrieve the single object [cite: 127, 129]
    [cite_start]
    public static ConfigurationManager GetInstance() // [cite: 128]
    {
        if (_instance == null)
        {
            [cite_start] lock (_lock) // Thread-safe implementation 
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
            }
        }
        return _instance;
    }
}

class Program
{
    static void Main()
    {
        [cite_start]// Access instance using ConfigurationManager.GetInstance() [cite: 143, 144]
        var config1 = ConfigurationManager.GetInstance();
        var config2 = ConfigurationManager.GetInstance();

        [cite_start]// Print configuration details [cite: 145]
        Console.WriteLine($"App Name: {config1.ApplicationName}, Version: {config1.Version}");

        [cite_start]// Demonstrate that multiple calls to GetInstance() return the same instance [cite: 134]
        Console.WriteLine($"Are both instances the same? {ReferenceEquals(config1, config2)}");
    }
}