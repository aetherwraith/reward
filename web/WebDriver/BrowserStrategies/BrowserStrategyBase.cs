using OpenQA.Selenium;

namespace web.WebDriver.BrowserStrategies
{
    /// <summary>   A browser strategy base. </summary>
    public abstract class BrowserStrategyBase
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the capabilities. </summary>
        /// <returns>   The capabilities. </returns>
        /// -------------------------------------------------------------------------------------------------
        public abstract ICapabilities GetCapabilities();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the options. </summary>
        /// <returns>   The options. </returns>
        /// -------------------------------------------------------------------------------------------------
        public abstract DriverOptions GetOptions();
    }
}