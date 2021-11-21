using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestsCalculator
{
    public class BaseTests
    {
        protected IWebDriver driver;

        public void OpenDriver()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://127.0.0.1:8080/";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
