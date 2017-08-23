using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Test.PageObjects;
using web.WebDriver;

namespace Test
{
    public class AdminReviewTests
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
            // Login as an admin user
            LoginPage.username.SendKeys("adminUser");
            LoginPage.password.SendKeys("adminPass");
            LoginPage.ok.Click();
            // Go to review tab
            AdminPage.reviews.Click();
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
        public void AddReviewer()
        {
            // Add new reviewer
            ReviewTab.reviewers.Select().SelectByText("aReviewer");
            ReviewTab.addButton.Click();
            
            // Endure new reviewer appears in list of reviwers
            ReviewTab.reviews.FindElement(By.XPath(".//td[contains(text, 'aReviewer')]")).Displayed.Should().BeTrue();
        }
        
        // Add tests that include changing users so that the displayed reviews should change
        // Check correct information is displayed in reviews table column
        // Ensure error is given when adding an already added reviewer

    }
}