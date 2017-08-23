using OpenQA.Selenium;
using web;

namespace Test.PageObjects
{
    public class AdminPage : BasePage
    {
        public static Element addEmployee = By.Id("addTab");
        public static Element updateEmployee = By.Id("updateTab");
        public static Element reviews = By.Id("reviews");
    }
}