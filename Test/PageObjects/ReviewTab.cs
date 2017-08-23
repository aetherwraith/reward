using OpenQA.Selenium;
using web;

namespace Test.PageObjects
{
    public class ReviewTab
    {
        public static Element chooseEmployee = By.Id("employee");
        public static Element reviews = By.Id("reviews"); // table of existing reviews with their comments
        public static Element reviewers = By.Id("reviewers");
        public static Element addButton = By.Id("add");
    }
}