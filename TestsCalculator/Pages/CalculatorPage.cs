using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestsCalculator.Pages
{
    public class CalculatorPage
    {
        private IWebDriver _driver;

        public CalculatorPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement AmountField => _driver.FindElement(By.Id("amount"));
        public IWebElement PercentField => _driver.FindElement(By.Id("percent"));
        public IWebElement TermField => _driver.FindElement(By.Id("term"));
        public SelectElement Day => new SelectElement(_driver.FindElement(By.Id("day")));
        public SelectElement Month => new SelectElement(_driver.FindElement(By.Id("month")));
        public SelectElement Year => new SelectElement(_driver.FindElement(By.Id("year")));
        public IWebElement DaysRadioBtn365 => _driver.FindElement(By.Id("d365"));
        public IWebElement DaysRadioBtn360 => _driver.FindElement(By.Id("d360"));
        public string IncomeField => _driver.FindElement(By.Id("income")).GetAttribute("value");
        public string InterestField => _driver.FindElement(By.Id("interest")).GetAttribute("value");
        public string EndDateField => _driver.FindElement(By.Id("endDate")).GetAttribute("value");
    }
}
