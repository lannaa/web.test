using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestsCalculator.Pages
{
    public class SettingsPage : BasePage
    {
        public SettingsPage(IWebDriver driver) : base(driver) 
        {
            PageName = "Settings";
        }

        public string DateText => _driver.FindElement(By.XPath("//th[text()='Date format:']")).Text;
        public string NumberText => _driver.FindElement(By.XPath("//th[text()='Number format:']")).Text;
        public string CurrencyText => _driver.FindElement(By.XPath("//th[text()='Default currency:']")).Text;
        public SelectElement DateFormat => new SelectElement(_driver.FindElement(By.Id("dateFormat")));
        public SelectElement NumberFormat => new SelectElement(_driver.FindElement(By.Id("numberFormat")));
        public SelectElement Currency => new SelectElement(_driver.FindElement(By.Id("currency")));
        public IWebElement SaveBtn => _driver.FindElement(By.Id("save"));
        public IWebElement CancelBtn => _driver.FindElement(By.Id("cancel"));
        public IWebElement LogoutBtn => _driver.FindElement(By.XPath("//div[text()='Logout']"));

        public void Save()
        {
            SaveBtn.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Accept();
        }
    }
}
