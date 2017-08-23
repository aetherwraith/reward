using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Utils;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>
    ///     Set up the Chrome Device Emulation Mode and it's driver executable.
    /// </summary>
    public class ChromeEmulationStrategy : BrowserStrategyBase, IBrowserStrategy
    {
        private readonly string _emulationDevice;

        /// <summary>
        ///     Default Initialiser for ChromeEmulationStrategy. Will attempt to read the device to be
        ///     emulated from the app.config file.
        /// </summary>
        public ChromeEmulationStrategy()
        {
            _emulationDevice = Config.ReadSetting("ChromeEmulationDevice");
        }

        /// <summary>
        ///     Initialiser for ChromeEmulationStrategy. Sets the device to be emulated based on the
        ///     supplied string.
        /// </summary>
        public ChromeEmulationStrategy(string emulationDevice)
        {
            _emulationDevice = emulationDevice;
        }

        /// <inheritdoc />
        public IWebDriver GetDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver((ChromeOptions) GetOptions());
        }

        public override ICapabilities GetCapabilities()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override DriverOptions GetOptions()
        {
            if (string.IsNullOrWhiteSpace(_emulationDevice))
                throw new ArgumentException("No mobile device specified for chrome to emulate.");
            var options = new ChromeOptions {Proxy = DriverProvider.Proxy};
            options.EnableMobileEmulation(_emulationDevice);
            return options;
        }
    }
}