using OpenQA.Selenium;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>   Interface for browser strategy. </summary>
    public interface IBrowserStrategy
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the capabilities. </summary>
        /// <returns>   The capabilities. </returns>
        /// -------------------------------------------------------------------------------------------------
        ICapabilities GetCapabilities();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the driver. </summary>
        /// <returns>   The driver. </returns>
        /// -------------------------------------------------------------------------------------------------
        IWebDriver GetDriver();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the options. </summary>
        /// <returns>   The options. </returns>
        /// -------------------------------------------------------------------------------------------------
        DriverOptions GetOptions();
    }
}