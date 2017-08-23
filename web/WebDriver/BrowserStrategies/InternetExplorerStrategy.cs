using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>
    ///     Set up Microsoft Internet Explorer.
    /// </summary>
    public class InternetExplorerStrategy : BrowserStrategyBase, IBrowserStrategy
    {
        /// <inheritdoc />
        public IWebDriver GetDriver()
        {
            new DriverManager().SetUpDriver(new InternetExplorerConfig(), "Latest", Architecture.X32);
            return new InternetExplorerDriver((InternetExplorerOptions) GetOptions());
        }

        public override ICapabilities GetCapabilities()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override DriverOptions GetOptions()
        {
            var options = new InternetExplorerOptions
            {
                Proxy = DriverProvider.Proxy,
                InitialBrowserUrl = "about:blank",
                RequireWindowFocus = true,
                EnsureCleanSession = true,
                EnablePersistentHover = false,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };
            return options;
        }
    }
}