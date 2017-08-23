using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>
    ///     Sets up the Chrome browser and it's executable driver.
    /// </summary>
    public class ChromeStrategy : BrowserStrategyBase, IBrowserStrategy
    {
        /// <inheritdoc />
        public IWebDriver GetDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver((ChromeOptions) GetOptions());
        }

        public override ICapabilities GetCapabilities()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override DriverOptions GetOptions()
        {
            var options = new ChromeOptions {Proxy = DriverProvider.Proxy};
            options.AddArgument("--disable-extensions");
            options.AddArgument("--start-maximized");
            return options;
        }
    }
}