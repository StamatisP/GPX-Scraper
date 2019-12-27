using System;
using System.Diagnostics;
using GPX_Scraper;

namespace TestScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a username.");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter a pokemon ID (https://gpx.plus/info/-->fiwZp<--)");
            string pokeid = Console.ReadLine();
            var scraper = new GPX_Scraper.Functions();

            var userd = Stopwatch.StartNew();
            scraper.GetUserData(username);
            userd.Stop();
            Console.WriteLine("UserData Elapsed={0}", userd.Elapsed);

            var poked = Stopwatch.StartNew();
            scraper.GetPokeData(pokeid);
            poked.Stop();
            Console.WriteLine("PokeData Elapsed={0}", poked.Elapsed);
            Console.WriteLine("Done! Please type \"exit\" to safely close the program.");
            var text = Console.ReadLine();
            if (text == "exit")
            {
                scraper.driver.Quit();
                Environment.Exit(1);
            }
        }
    }
}
