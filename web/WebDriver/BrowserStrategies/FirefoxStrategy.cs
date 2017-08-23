using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>
    ///     Set up Firefox browser.
    /// </summary>
    public class FirefoxStrategy : BrowserStrategyBase, IBrowserStrategy
    {
        /// <inheritdoc />
        public IWebDriver GetDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver((FirefoxOptions) GetOptions());
        }

        public override ICapabilities GetCapabilities()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override DriverOptions GetOptions()
        {
            var profile = new FirefoxProfile();
            profile.SetPreference("browser.tabs.remote.autostart.2", false);
            profile.SetPreference("security.insecure_field_warning.contextual.enabled", false);
            profile.SetProxyPreferences(DriverProvider.Proxy);
            profile.DeleteAfterUse = true;

            var options = new FirefoxOptions {Profile = profile};

            return options;
        }
    }
}