using System;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestsCalculator.Pages
{
    public class CalculatorPage : BasePage
    {
        public CalculatorPage(IWebDriver driver): base(driver)
        {
            PageName = "Deposite calculator";
        }

        public IWebElement AmountField => _driver.FindElement(By.Id("amount"));
        public IWebElement PercentField => _driver.FindElement(By.Id("percent"));
        public IWebElement TermField => _driver.FindElement(By.Id("term"));
        public SelectElement Day => new SelectElement(_driver.FindElement(By.Id("day")));
        public SelectElement Month => new SelectElement(_driver.FindElement(By.Id("month")));
        public SelectElement Year => new SelectElement(_driver.FindElement(By.Id("year")));
        public IWebElement DaysRadioBtn365 => _driver.FindElement(By.Id("d365"));
        public IWebElement DaysRadioBtn360 => _driver.FindElement(By.Id("d360"));
        public string Income => _driver.FindElement(By.Id("income")).GetAttribute("value");
        public string Interest => _driver.FindElement(By.Id("interest")).GetAttribute("value");
        public string EndDate => _driver.FindElement(By.Id("endDate")).GetAttribute("value");
        public string CurrencySymbol => _driver.FindElement(By.XPath("(//td [@id='currency'])")).Text;
        public IWebElement SettingsBtn => _driver.FindElement(By.XPath("//div[text()='Settings']")); 

        public string StartDate
        {
            get
            {
                int day = int.Parse(Day.SelectedOption.Text);
                int month = DateTime.ParseExact(Month.SelectedOption.Text, "MMMM", CultureInfo.InvariantCulture).Month;
                int year = int.Parse(Year.SelectedOption.Text);
                return $"{day}/{month}/{year}";

            }
            set
            {
                DateTime date = DateTime.Parse(value);
                Day.SelectByText(date.Day.ToString());
                Month.SelectByText(date.ToString("MMMM"));
                Year.SelectByText(date.ToString("yyyy"));
            }
        }
    }
}
