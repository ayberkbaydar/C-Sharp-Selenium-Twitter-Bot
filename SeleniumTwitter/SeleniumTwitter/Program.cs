using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            string userId = Twitter.readFile(@"C:\Users\{kullanici_Adi}\Desktop\SeleniumTwitter\SeleniumTxt\username.txt");
            string userPass = Twitter.readFile(@"C:\Users\{kullanici_Adi}\Desktop\SeleniumTwitter\SeleniumTxt\password.txt");
            Twitter twit = new Twitter(userId,userPass);
            twit.signIn();
            Thread.Sleep(2000);
            //twit.search("test hashtag");
            //twit.sendTweet("test tweets");
            string twitText = Twitter.readFile(@"C:\Users\{kullanici_Adi}\Desktop\SeleniumTwitter\SeleniumTxt\tweets.txt");
            twit.sendTweet(twitText);
        }
    }

    public class Twitter
    {
        public string user, pass;
        public IWebDriver driver;
        public Twitter(string username,string password)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://twitter.com/login");
            user = username;
            pass = password;
        }

        public void signIn()
        {
            driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div[1]/form/div/div[1]/label/div/div[2]/div/input")).SendKeys(user);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div[1]/form/div/div[2]/label/div/div[2]/div/input")).SendKeys(pass);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div[1]/form/div/div[3]/div/div/span/span")).Click();
            driver.Manage().Window.Maximize();
        }

        public void search(string hashtag)
        {
            IWebElement searchElement = driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div/div[2]/div/div[2]/div/div/div/div[1]/div/div/div/form/div[1]/div/div/div[2]/input"));
            searchElement.SendKeys(hashtag);
            Thread.Sleep(1000);
            searchElement.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
        }

        public void sendTweet(string tweets)
        {
            driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div[2]/div[1]/div/div/div/div[2]/div[1]/div/div/div/div/div/div/div/div/div/div[1]/div/div/div/div[2]/div/div/div/div")).SendKeys(tweets);
            driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div[2]/div[1]/div/div/div/div[2]/div[4]/div/div/div[2]/div[3]/div/span/span")).Click();
        }

        public static string readFile(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string text = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                return text;
            }
            catch (Exception)
            {
                Console.WriteLine("Dosya yolu bulunamıyor.");
                throw;
            }
            
        }
    }
}
