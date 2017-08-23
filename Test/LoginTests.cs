using FluentAssertions;
using NUnit.Framework;
using Test.PageObjects;
using web.WebDriver;

namespace Test
{
    [TestFixture]
    public class LoginTests
    {
        [OneTimeSetUp]
        public void Init()
        {
            // Start the browser
            DriverProvider.Browser = "Chrome";
            DriverProvider.Initialize();
        }
        
        [SetUp]
        public void SetUp()
        {
            // Before each test go to the login page
            DriverProvider.GetDriver().Navigate().GoToUrl("url.of.login.page");
        }

        [TearDown]
        public void TearDown()
        {
            // Logout after each test
            BasePage.logout.Click();
            LoginPage.ok.IsDisplayed();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // Close the driver
            DriverProvider.End();
        }

        [Test]
        public void AdminLogin()
        {
            // Login as an admin user
            LoginPage.username.SendKeys("adminUser");
            LoginPage.password.SendKeys("adminPass");
            LoginPage.ok.Click();

            // Ensure we are on the admin user's homepage
            DriverProvider.GetDriver().Title.Should().Be("Welcom adminUser");
        }
        
        // Add more tests here to determine if password length/complexity is an issue
        // Test edge cases such as very long usernames/passwords
        // Can use test fixtures to parametrise and generate the invalid login details
        // Check non-admin user cannot access admin pages
    }
}