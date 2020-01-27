using System;

namespace SomeCodeTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CsvFileProcessor csvFileProcessor = new CsvFileProcessor();
            //csvFileProcessor.WriteCsvFile(@"D:\Test\csvFile.csv");
            csvFileProcessor.CreateCSVFile(@"D:\Test\csvFile.csv");
            Console.WriteLine("Done");
            //Console.ReadLine();
        }
    }
}
