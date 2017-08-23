using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Utils;

namespace web.WebDriver
{
    /// <summary>
    ///     Holds the web driver <see langword="interface" /> and associated
    ///     properties.
    /// </summary>
    public static class DriverProvider
    {
        private static readonly CommonLoggingLogger Logger = CommonLoggingLogger.Instance;

        /// <summary>
        ///     The <see cref="SupportedBrowsers.Browser" /> type to use during
        ///     testing.
        /// </summary>
        private static SupportedBrowsers.Browser _browser;

        static DriverProvider()
        {
            // Initialise static member variables
            Browser = Config.ReadSetting("Browser");
            Logger.Debug($"Browser set to: {Browser}");
            BrowserVersion = Config.ReadSetting("BrowserVersion");
            Logger.Debug($"Browser version set to: {BrowserVersion}");
            Platform = Config.ReadSetting("Platform");
            Logger.Debug($"Platform set to: {Platform}");
            Proxy = SetProxy();
            Grid = Config.ReadSetting("Grid");
            Logger.Debug($"Grid set to: {Grid}");
        }

        /// <summary>
        ///     This property access the
        ///     <see cref="web.WebDriver.DriverProvider._browser" /> string from
        ///     thread local storage.
        /// </summary>
        public static string Browser
        {
            get => Enum.GetName(typeof(SupportedBrowsers.Browser), _browser);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _browser = SupportedBrowsers.DefaultBrowser;
                    return;
                }
                if (Enum.TryParse(value, true, out _browser)) return;
                Logger.Debug(value + " is not a valid browser");
                throw new ArgumentOutOfRangeException(value + " is not a valid browser");
            }
        }

        /// <summary>
        ///     Stores the desired
        ///     <see cref="web.WebDriver.DriverProvider._browser" /> version.
        /// </summary>
        public static string BrowserVersion { get; set; }

        /// <summary>
        ///     URL of grid/selenium remote instance.
        /// </summary>
        public static string Grid { get; set; }

        /// <summary>
        ///     Stores the desired platform.
        /// </summary>
        public static string Platform { get; set; }

        /// <summary>
        ///     This property accesses the proxy from thread local storage.
        /// </summary>
        public static Proxy Proxy { get; set; }

        /// <summary>
        ///     Obtain the selenium web driver.
        /// </summary>
        public static IWebDriver WebDriver { get; set; }

        /// <summary>
        ///     Stops the webdriver
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        /// <exception cref="Exception" />
        public static void End()
        {
            Logger.Debug("Attempting to stop driver instance.");
            try
            {
                if (WebDriver == null)
                {
                    Logger.Debug("No driver was started.");
                    throw new InvalidOperationException("No driver was started.");
                }

                Logger.Debug($"Current driver is: {WebDriver}");

                // Clean up the web driver
                WebDriver.Dispose();
                WebDriver = null;
            }
            catch (Exception ex)
            {
                // Log exception
                Logger.Debug("Error closing down webdriver.", ex);

                // Rethrow the exception
                throw;
            }
            Logger.Debug("Webdriver sucessfully stopped.");
        }

        /// <summary>
        ///     Get the current webdriver instance
        /// </summary>
        /// <returns>
        /// </returns>
        public static IWebDriver GetDriver()
        {
            return WebDriver;
        }

        /// <summary>
        ///     Get platform to run on
        /// </summary>
        /// <returns>
        /// </returns>
        public static string GetPlatform()
        {
            Logger.Debug("Getting platform of current browser.");
            var capabilities = ((RemoteWebDriver) WebDriver).Capabilities;
            Logger.Debug($"Current platform is: {capabilities.Platform}");
            return capabilities.Platform.ToString();
        }

        /// <summary>
        ///     <para>
        ///         Return a formatted string with details of the
        ///         <see cref="web.WebDriver.DriverProvider._browser" />
        ///     </para>
        ///     <para>and the version.</para>
        /// </summary>
        /// <returns>
        ///     The formatted string.
        /// </returns>
        public static string GetRunningBrowserAndVersion()
        {
            Logger.Debug("Getting name and version of current browser.");
            // Get the browser capabilities
            var capabilities = ((RemoteWebDriver) WebDriver).Capabilities;
            Logger.Debug($"Current running browser is {capabilities.BrowserName} {capabilities.Version}");

            // Format the string
            return $"{capabilities.BrowserName} {capabilities.Version}";
        }

        /// <summary>
        ///     Set the webdriver and start the
        ///     <see cref="web.WebDriver.DriverProvider._browser" />
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        /// <exception cref="Exception" />
        public static void Initialize()
        {
            Logger.Debug($"Starting browser {Browser}.");
            try
            {
                WebDriver = WebDriverFactory.Init(_browser);
                if (WebDriver == null) throw new InvalidOperationException("could not set webdriver");
            }
            catch (Exception ex)
            {
                Logger.Debug("Error initialising driver.", ex);
                throw;
            }

            Logger.Debug("Maximising browser window.");
            WebDriver.Manage().Window.Maximize();

            Logger.Debug($"Browser, {Browser}, started.");
        }

        /// <summary>
        ///     Save a screenshot in jpeg format to a default file.
        ///     File is timestamped and saved in configured screenshot folder.
        /// </summary>
        /// <returns></returns>
        public static string SaveScreenshot()
        {
            Logger.Debug("Saving screenshot to default folder.");
            var screenshotPath = Config.ReadSetting("ScreenshotPath");
            if (string.IsNullOrEmpty(screenshotPath))
                screenshotPath = "screenshots";

            var screenshotFile = Path.Combine(Config.GetOutputDir(), screenshotPath,
                DateTime.Now.ToString("yyyyMMddTHHmmss") + ".jpg");
            SaveScreenshotTo(screenshotFile);
            return screenshotFile;
        }

        /// <summary>
        ///     Save a jpeg screenshot to the supplied file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        public static void SaveScreenshotTo(string path)
        {
            // Call the overload using a jpeg
            SaveScreenshotTo(path, ScreenshotImageFormat.Jpeg);
        }

        /// <summary>
        ///     Save a screenshot to the specified file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="imageFormat"></param>
        public static void SaveScreenshotTo(string path, ScreenshotImageFormat imageFormat)
        {
            Logger.Debug($"Saving screenshot to {path}");

            // Take the screenshot            
            var screenshot = ((ITakesScreenshot) WebDriver).GetScreenshot();
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Save to the supplied file
            screenshot.SaveAsFile(path, imageFormat);
            Logger.Debug($"Screenshot saved to {path}");
        }

        private static Proxy SetProxy()
        {
            var proxyUrl = Config.ReadSetting("Proxy");
            Logger.Debug($"Setting proxy to: {proxyUrl}");
            if (string.IsNullOrWhiteSpace(proxyUrl)) return new Proxy {Kind = ProxyKind.System};
            var proxy = new Proxy
            {
                Kind = ProxyKind.Manual,
                FtpProxy = proxyUrl,
                HttpProxy = proxyUrl,
                SslProxy = proxyUrl
            };
            Logger.Debug("Proxy set.");
            return proxy;
        }
    }
}