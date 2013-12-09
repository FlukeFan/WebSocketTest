using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebSocketTest
{
    [TestFixture]
    public class OpenSocketTest
    {
        [Test]
        public void OpenManyTimes()
        {
            Server.Start();

            using (var webServer = StartWebServer())
            using (var browser = OpenBrowser())
            {
                for (int i = 0; i < 300; i++)
                {
                    browser.Navigate().GoToUrl("http://localhost:8091/Client.html");

                    Wait.For(() =>
                    {
                        var done = browser.FindElement(By.Id("done"));
                        Assert.That(done.Text, Is.EqualTo("Waiting"));
                    });

                    browser.FindElement(By.Id("start")).Click();

                    Wait.For(() =>
                    {
                        var done = browser.FindElement(By.Id("done"));
                        Assert.That(done.Text, Is.EqualTo("Complete"));
                    });
                }
            }
        }

        private CassiniDev.Server StartWebServer()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../.."));
            var webServer = new CassiniDev.Server(8091, path);
            webServer.Start();
            return webServer;
        }

        private IWebDriver OpenBrowser()
        {
            var profile = new FirefoxProfile(@"..\..\..\..\tools\FirefoxPortable\Data\profile");
            var binary = new FirefoxBinary(@"..\..\..\..\tools\FirefoxPortable\App\Firefox\firefox.exe");
            var browser = new FirefoxDriver(binary, profile, TimeSpan.FromMinutes(5));
            return browser;
        }
    }
}
