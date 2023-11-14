using System;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string apiKey = configuration["AppSettings:OutputPath"];
        

        Console.WriteLine($"ApiKey: {apiKey}");

        Console.ReadLine();
        // Rest of your console application logic
    }
}
