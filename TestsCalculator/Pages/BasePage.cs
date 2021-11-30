using OpenQA.Selenium;

namespace TestsCalculator.Pages
{
    public class BasePage
    {
        protected IWebDriver _driver;
        protected string PageName;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsOpened => _driver.Title.Equals(PageName);
    }
}
