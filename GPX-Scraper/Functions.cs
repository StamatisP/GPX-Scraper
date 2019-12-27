using System;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GPX_Scraper
{
    public struct PokeData
    {
        public string Name;
        public string Nickname;
        public string Nature;
        public string Sprite; // URL
        public string Gender;
        public string Owner;
    }
    public struct UserData
    {
        public string Name;
        public string Journal;
        public string Status;
        public string Affinity;
        public PokeData[] Party;
    }
    public class Functions
    {
        public IWebDriver driver;
        public Functions()
        {
            //var o = new FirefoxOptions();
            var o = new ChromeOptions();
            o.AddArgument("headless");
            o.AddArgument("window-size=1200x600");
            //o.AddArgument("-headless");
            driver = new ChromeDriver(o);
        }
        public UserData GetUserData(string UserName)
        {
            UserData data = new UserData();
            driver.Navigate().GoToUrl("https://gpx.plus/user/" + UserName);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.Id("userInfo"))); // confirm connected

            //Name
            var namew = Stopwatch.StartNew();
            IWebElement element = driver.FindElement(By.Id("userInfo"));
            //Console.WriteLine(element.Text);
            data.Name = element.FindElement(By.TagName("div")).Text.Trim();
            namew.Stop();
            Console.WriteLine("User Name Elapsed={0}", namew.Elapsed);

            //Journal
            var journalw = Stopwatch.StartNew();
            element = driver.FindElement(By.Id("userJournal"));
            element = element.FindElement(By.TagName("article"));
            data.Journal = element.Text;
            journalw.Stop();
            Console.WriteLine("Journal Elapsed={0}", journalw.Elapsed);

            //Status
            var statusw = Stopwatch.StartNew();
            element = driver.FindElement(By.Id("userStatus"));
            element = element.FindElement(By.TagName("div"));
            element = element.FindElement(By.TagName("span"));
            data.Status = element.Text;
            statusw.Stop();
            Console.WriteLine("User Status Elapsed={0}", statusw.Elapsed);

            #region Affinity
            element = driver.FindElement(By.Id("userInfo"));
            try
            {
                element = element.FindElement(By.CssSelector("[class^=SpriteAffinity]"));
                string affinity = element.GetAttribute("class").Remove(0, 14);
                data.Affinity = affinity;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("No affinity found!");
            }
            #endregion

            //Party
            var partyw = Stopwatch.StartNew();
            element = driver.FindElement(By.Id("UserParty"));
            var party = driver.FindElements(By.ClassName("PartyPoke"));
            PokeData[] newParty;
            for (int i = 0; i < party.Count; i++)
            {
                // Name
                //party[i].
            }
            partyw.Stop();
            Console.WriteLine("User Party Elapsed={0}", partyw.Elapsed);

            return data;
        }

        public PokeData GetPokeData(string dataName)
        {
            PokeData pokedata = new PokeData();

            driver.Navigate().GoToUrl("https://gpx.plus/info/" + dataName);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.Id("infoTable"))); // confirm connected

            #region Nickname
            IWebElement element = driver.FindElement(By.Id("infoPokemon"));
            element = element.FindElement(By.TagName("em"));
            pokedata.Nickname = element.Text;
            #endregion

            #region Name
            element = driver.FindElement(By.Id("infoPokemon"));
            var emelements = element.FindElements(By.TagName("em"));
            pokedata.Name = emelements[1].Text.Trim().Remove(0, 1); // get rid of dash
            #endregion

            #region Nature
            element = driver.FindElement(By.Id("infoPokemon"));
            element = element.FindElement(By.ClassName("tip"));
            pokedata.Nature = element.Text;
            #endregion

            #region Sprite
            element = driver.FindElement(By.Id("infoPokemon"));
            element = element.FindElement(By.TagName("img"));
            pokedata.Sprite = element.GetAttribute("src");
            #endregion

            #region Gender
            element = driver.FindElement(By.Id("infoPokemon"));
            element = element.FindElement(By.TagName("figure"));
            try
            {
                element = element.FindElement(By.CssSelector("[class^=SpriteGender]"));
                string gender = element.GetAttribute("class").Remove(0, 12);
                Console.WriteLine(gender);
                pokedata.Gender = gender;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("No spritegender found, must be genderless");
                pokedata.Gender = "Genderless";
            }
            #endregion

            #region Owner
            element = driver.FindElement(By.Id("infoPokemon"));
            element = element.FindElement(By.TagName("a"));
            #endregion

            return pokedata;
        }
        
    }
}
