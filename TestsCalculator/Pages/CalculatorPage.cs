using System;
using OpenQA.Selenium;

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
        public IWebElement DayDropdown => _driver.FindElement(By.Id("day"));
        public IWebElement MonthDropdown => _driver.FindElement(By.Id("month"));
        public IWebElement YearDropdown => _driver.FindElement(By.Id("year"));
        public IWebElement DaysRadioBtn365 => _driver.FindElement(By.Id("d365"));
        public IWebElement DaysRadioBtn360 => _driver.FindElement(By.Id("d360"));
        public IWebElement IncomeField => _driver.FindElement(By.Id("income"));
        public IWebElement InterestField => _driver.FindElement(By.Id("interest"));
        public IWebElement EndDateField => _driver.FindElement(By.Id("endDate"));
    }
}
