using System;
using OpenQA.Selenium;
using Utils;
using web.WebDriver;

namespace web
{
    /// <summary>
    ///     Describes User <see cref="OpenQA.Selenium.Interactions.Actions" />
    /// </summary>
    public class UserActions
    {
        private readonly CommonLoggingLogger _logger = CommonLoggingLogger.Instance;

        /// <summary>
        ///     Default initialiser.
        /// </summary>
        public UserActions()
        {
        }

        /// <summary>
        ///     Method to hover an <paramref name="element" /> with the mouse
        ///     cursor.
        /// </summary>
        /// <param name="element">WebElement to hover</param>
        /// <param name="timeout">How long to wait</param>
        /// <param name="useJavascript">Try to use javascript?</param>
        public void MouseHover(IWebElement element, int timeout, bool useJavascript)
        {
            _logger.Debug("Attempting to hover over element.");
            try
            {
                var elem = WaitMethods.WaitForElementToBeDisplayed(element, timeout);
                if (useJavascript)
                {
                    _logger.Debug("Using javascript.");
                    var js = (IJavaScriptExecutor)DriverProvider.GetDriver();
                    js.ExecuteScript("arguments[0].onmouseover();", elem);
                }
                else
                {
                    var action = new OpenQA.Selenium.Interactions.Actions(DriverProvider.GetDriver());
                    action.MoveToElement(elem).Perform();
                }
            }
            catch (Exception ex)
            {
                _logger.Debug("Element could not be Hovered.", ex);
                throw;
            }
            _logger.Debug("Hovering over element.");
        }

        /// <summary>
        ///     Move the mouse to hover over the given element
        /// </summary>
        /// <param name="locator">Locator of WebElement to hover</param>
        /// <param name="timeout">How long to wait</param>
        /// <param name="useJavascript">Try to use javascript?</param>
        public void MouseHover(By locator, int timeout, bool useJavascript)
        {
            var elem = WaitMethods.WaitForElementToBePresent(locator, timeout);
            MouseHover(elem, timeout, useJavascript);
        }

    }
}