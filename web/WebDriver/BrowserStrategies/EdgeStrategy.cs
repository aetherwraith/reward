using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>
    ///     Sets up the Edge browser.
    /// </summary>
    public class EdgeStrategy : BrowserStrategyBase, IBrowserStrategy
    {
        /// <inheritdoc />
        public IWebDriver GetDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            return new EdgeDriver((EdgeOptions) GetOptions());
        }

        public override ICapabilities GetCapabilities()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override DriverOptions GetOptions()
        {
            var options = new EdgeOptions();
            options.AddAdditionalCapability(CapabilityType.Proxy, DriverProvider.Proxy);
            return options;
        }
    }
}