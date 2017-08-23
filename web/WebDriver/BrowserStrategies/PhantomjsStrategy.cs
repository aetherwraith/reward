using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>
    ///     Sets up the PhantomJS headless browser and it's executable driver.
    /// </summary>
    public class PhantomjsStrategy : BrowserStrategyBase, IBrowserStrategy
    {
        /// <inheritdoc />
        public IWebDriver GetDriver()
        {
            // TODO: Remove hardcoded phantom version here
            new DriverManager().SetUpDriver(new PhantomConfig(), "2.1.1");
            return new PhantomJSDriver((PhantomJSOptions) GetOptions());
        }

        public override ICapabilities GetCapabilities()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override DriverOptions GetOptions()
        {
            var options = new PhantomJSOptions();
            options.AddAdditionalCapability(CapabilityType.Proxy, DriverProvider.Proxy);
            return options;
        }
    }
}