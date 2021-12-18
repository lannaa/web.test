using NUnit.Framework;
using TestsCalculator.Pages;

namespace TestsCalculator
{
    public class LoginPageTests : BaseTests
    {
        [SetUp]
        public void SetUp() => OpenDriver();

        [Test]
        public void LoginPositiveTest()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            // Act
            loginPage.Login("test", "newyork1");

            // Assert
            Assert.IsTrue(calculatorPage.IsOpened);
        }

        [Test]
        public void NegativeWrongNameTest()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Act
            loginPage.LoginField.SendKeys("negativeTest");
            loginPage.PasswordField.SendKeys("newyork1");
            loginPage.LoginButton.Click();

            // Assert
            Assert.IsTrue(loginPage.InfoMessages.Displayed);
            Assert.AreEqual("Incorrect user name!", loginPage.InfoMessages.Text);
        }

        [Test]
        public void NegativeWrongPassTest()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Act
            loginPage.LoginField.SendKeys("test");
            loginPage.PasswordField.SendKeys("test");
            loginPage.LoginButton.Click();

            // Assert
            Assert.IsTrue(loginPage.InfoMessages.Displayed);
            Assert.AreEqual("Incorrect password!", loginPage.InfoMessages.Text);
        }

        [Test]
        public void NegativeWrongCredentialsTest()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);
   

            // Act
            loginPage.LoginField.SendKeys("testLogin");
            loginPage.PasswordField.SendKeys("testPass");
            loginPage.LoginButton.Click();

            // Assert
            Assert.IsTrue(loginPage.InfoMessages.Displayed);
            Assert.AreEqual("\'testLogin\' user doesn\'t exist!", loginPage.InfoMessages.Text);
        }

        [Test]
        public void PositiveRemindPassPresentTest()
        {
            // Arrange
            LoginPage loginPage = new LoginPage(driver);

            // Assert
            Assert.IsTrue(loginPage.RemindPassBtn.Displayed);
        }
    }
}
