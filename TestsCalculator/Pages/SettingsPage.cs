using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestsCalculator.Pages
{
    public class SettingsPage
    {
        private IWebDriver _driver;

        public SettingsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement DateFormat => _driver.FindElement(By.XPath("//th[text()=\"Date format:\"]"));
        public SelectElement DateFormatOptions => new SelectElement(_driver.FindElement(By.Id("dateFormat")));
        public IWebElement NumberFormat => _driver.FindElement(By.XPath("//th[text()=\"Number format:\"]"));
        public SelectElement NumberFormatOptions => new SelectElement(_driver.FindElement(By.Id("numberFormat")));
        public IWebElement Currency => _driver.FindElement(By.XPath("//th[text()=\"Defalut currency:\"]"));
        public SelectElement CurrencyFormatOptions => new SelectElement(_driver.FindElement(By.Id("currency")));
        public IWebElement SaveBtn => _driver.FindElement(By.Id("save"));
        public IWebElement CancelBtn => _driver.FindElement(By.Id("cancel"));
        public IWebElement CurrencySymbol => _driver.FindElement(By.Id("currency"));
    
        }
    }

