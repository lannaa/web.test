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
        public IWebElement NumberFormat => _driver.FindElement(By.XPath("//th[text()=\"Number format:\"]"));
        // to do - correct a typo 
        public IWebElement Currency => _driver.FindElement(By.XPath("//th[text()=\"Defalut currency:\"]"));
    
        public void OpenSettingsPage()
        {

        }

       
        }
    }

