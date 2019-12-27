using System;
using System.Diagnostics;
using GPX_Scraper;

namespace TestScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var scraper = new GPX_Scraper.Functions();

            var userd = Stopwatch.StartNew();
            scraper.GetUserData("StamosYeah");
            userd.Stop();
            Console.WriteLine("UserData Elapsed={0}", userd.Elapsed);

            var poked = Stopwatch.StartNew();
            scraper.GetPokeData("fiwZp");
            poked.Stop();
            Console.WriteLine("PokeData Elapsed={0}", poked.Elapsed);
            Console.WriteLine("Done!");
            var text = Console.ReadLine();
            if (text == "exit")
            {
                scraper.driver.Quit();
                Environment.Exit(1);
            }
        }
    }
}
