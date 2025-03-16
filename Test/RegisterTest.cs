using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace GroceryManagement.Tests
{
    [TestFixture]
    public class RegisterTests
    {
        private IWebDriver _driver;

        // Initialize the ChromeDriver
        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();  
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
        }

        // Clean up after each test
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        // Test for the Register functionality
        [Test]
        public void Register_NewUser_SuccessfulRegistration()
        {
            // Navigate to the Register page
            _driver.Navigate().GoToUrl("https://localhost:44384/DeliveryPerson/Register");

            // Find the necessary fields and interact with them
            _driver.FindElement(By.Id("Username")).SendKeys("testuser");
            _driver.FindElement(By.Id("Password")).SendKeys("TestPassword123");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("TestPassword123");
            _driver.FindElement(By.Id("Email")).SendKeys("testuser@example.com");
            _driver.FindElement(By.Id("PhoneNumber")).SendKeys("1234567890");

            // Click the Register button
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            // Wait for the page to load and verify the result
            Thread.Sleep(3000);  

            // Verify if the user is redirected to Login page or a success message is shown
            string currentUrl = _driver.Url;
            Assert.True(currentUrl.Contains("Login"));
        }

        // Test for unsuccessful registration (e.g., empty fields )
        [Test]
        public void Register_ExistingUser_FailsRegistration()
        {
            // Navigate to the Register page
            _driver.Navigate().GoToUrl("https://localhost:44384/DeliveryPerson/Register");

            // Fill out the registration form with empty username or password
            _driver.FindElement(By.Id("Username")).SendKeys("");
            _driver.FindElement(By.Id("Password")).SendKeys("");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("");
            _driver.FindElement(By.Id("Email")).SendKeys("");
            _driver.FindElement(By.Id("PhoneNumber")).SendKeys("");

            // Click the Register button
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            // Wait for the page to load and check for error message (you can verify the error or warning displayed)
            Thread.Sleep(3000);

            // Verify that the error message is displayed
            string errorMessage = _driver.FindElement(By.Id("error-message")).Text;  
            Assert.AreEqual("Username already exists", errorMessage); 
        }
    }
}
