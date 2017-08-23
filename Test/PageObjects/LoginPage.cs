using OpenQA.Selenium;
using web;

namespace Test.PageObjects
{
    public class LoginPage
    {
        public static Element username = By.Id("username");
        public static Element password = By.Id("password");
        public static Element ok = By.Id("ok");
    }
}