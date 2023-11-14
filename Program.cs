using System;
using Microsoft.Extensions.Configuration;



namespace ImageApp
{
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



            // FileDataProcess.FileMain();
            Console.WriteLine("Choose Option :");
            Console.WriteLine("1. Image Segregation");
            Console.WriteLine("2. Video Segregation");

            
            FileProcess program = new FileProcess();
            program.FileRead(1);
            program.FileRead(2);

            Console.ReadLine();
            // Rest of your console application logic
        }

        
    }
}