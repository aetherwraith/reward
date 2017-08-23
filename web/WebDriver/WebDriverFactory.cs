using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Utils;
using web.WebDriver.BrowserStrategies;

namespace web.WebDriver
{
    /// <summary>
    ///     Set up and instantiate a web
    /// </summary>
    public static class WebDriverFactory
    {
        private static readonly CommonLoggingLogger Logger = CommonLoggingLogger.Instance;

        static WebDriverFactory()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="browser"></param>
        /// <exception cref="System.NotImplementedException" />
        /// <exception cref="System.ArgumentOutOfRangeException" />
        /// <returns>
        /// </returns>
        public static IWebDriver Init(SupportedBrowsers.Browser browser)
        {
            Logger.Debug($"Attempting to start browser: {browser}");
            IBrowserStrategy browserStrategy;
            switch (browser)
            {
                case SupportedBrowsers.Browser.Firefox:
                    browserStrategy = new FirefoxStrategy();
                    break;
                case SupportedBrowsers.Browser.Chrome:
                    browserStrategy = new ChromeStrategy();
                    break;
                case SupportedBrowsers.Browser.Iexplore:
                    browserStrategy = new InternetExplorerStrategy();
                    break;
                case SupportedBrowsers.Browser.Phantomjs:
                    browserStrategy = new PhantomjsStrategy();
                    break;
                case SupportedBrowsers.Browser.Chromeemulation:
                    browserStrategy = new ChromeEmulationStrategy();
                    break;
                case SupportedBrowsers.Browser.Edge:
                    browserStrategy = new EdgeStrategy();
                    break;
                case SupportedBrowsers.Browser.Opera:
                case SupportedBrowsers.Browser.Safari:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }

            var webDriver = string.IsNullOrWhiteSpace(DriverProvider.Grid)
                ? browserStrategy.GetDriver()
                : new RemoteWebDriver(new Uri(DriverProvider.Grid), browserStrategy.GetOptions());
            Logger.Debug($"Started browser: {browser}");
            return webDriver;
        }
    }
}