using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Utils;
using web.WebDriver;

namespace web
{
    /// <summary>   An element. </summary>
    public class Element : IWebElement
    {
        /// <summary>
        ///     The logger
        /// </summary>
        public readonly CommonLoggingLogger Logger = CommonLoggingLogger.Instance;

        /// <summary>   The locator. </summary>
        protected By Locator;


        /// <summary>   The timeout. </summary>
        protected int Timeout;


        /// <summary>   Default constructor. </summary>
        public Element()
        {
            Locator = By.XPath("//title");
            SetTimeout();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="locator">  The locator. </param>
        /// -------------------------------------------------------------------------------------------------
        public Element(By locator)
        {
            Locator = locator;
            SetTimeout();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="locator">  The locator. </param>
        /// <param name="timeout">  The timeout. </param>
        /// -------------------------------------------------------------------------------------------------
        public Element(By locator, int timeout)
        {
            Locator = locator;
            SetTimeout(timeout);
        }


        /// <summary>   True if present. </summary>
        public bool Present => IsPresent();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first element. </summary>
        /// <param name="by">   The by. </param>
        /// <returns>   The found element. </returns>
        /// -------------------------------------------------------------------------------------------------
        public IWebElement FindElement(By by)
        {
            Logger.Debug($"Finding element {by}");
            var element = WaitForElementToBePresent(Locator, Timeout);
            return element.FindElement(by);
        }

        /// <summary>
        /// To prevent having too many sleeps in tests use the WebDriverWait and ExpectedConditions
        /// to wait for an element to be present
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private static IWebElement WaitForElementToBePresent(By locator, int timeout)
        {
            var wait = new WebDriverWait(DriverProvider.GetDriver(), TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementExists(locator));
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the given elements. </summary>
        /// <param name="by">   The by. </param>
        /// <returns>   The found elements. </returns>
        /// -------------------------------------------------------------------------------------------------
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return FindElements(by, Timeout);
        }


        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Clears the content of this element. </summary>
        public virtual void Clear()
        {
            WaitForElementToBeClickable(Locator, Timeout).Click();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Clicks this element. </summary>
        /// -------------------------------------------------------------------------------------------------
        public virtual void Click()
        {
            Click(Timeout);
        }

        private void Click(int timeout)
        {
            WaitForElementToBeClickable(Locator, timeout).Click();
        }


        /// <summary>   True if displayed. </summary>
        public bool Displayed => IsDisplayed();


        /// <summary>   True if enabled. </summary>
        public bool Enabled => IsEnabled();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the value of the specified attribute for this element. </summary>
        /// <param name="attributeName">    The name of the attribute. </param>
        /// <returns>
        ///     The attribute's current value. Returns a <see langword="null" />
        ///     if the
        ///     value is not set.
        /// </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetAttribute(string attributeName)
        {
            return GetAttribute(attributeName, Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the value of a CSS property of this element. </summary>
        /// <param name="propertyName"> The name of the CSS property to get the value of. </param>
        /// <returns>   The value of the specified CSS property. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetCssValue(string propertyName)
        {
            return GetCssValue(propertyName, Timeout);
        }

        /// <summary>
        /// Provide a wrapper around the selenium select class
        /// </summary>
        /// <returns>A select element</returns>
        public SelectElement Select()
        {
            var elem = WaitForElementToBeClickable(Locator, Timeout);
            return new SelectElement(elem);
        }


        /// <summary>   The location. </summary>
        public Point Location => GetLocation();


        /// <summary>   True if selected. </summary>
        public bool Selected => IsSelected();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Simulates typing text into the element. </summary>
        /// <param name="text"> The text to type into the element. </param>
        /// <seealso cref="T:OpenQA.Selenium.Keys" />
        /// -------------------------------------------------------------------------------------------------
        public void SendKeys(string text)
        {
            SendKeys(text, Timeout);
        }


        /// <summary>   The size. </summary>
        public Size Size => GetSize();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Submits this element to the web server. </summary>
        /// -------------------------------------------------------------------------------------------------
        public virtual void Submit()
        {
            Submit(Timeout);
        }

        public virtual void Submit(int timeout){            var elem = WaitForElementToBeDisplayed(Locator, timeout);
            elem.Submit();
        }


        /// <summary>   Name of the tag. </summary>
        public string TagName => GetTagName();


        /// <summary>   The text. </summary>
        public string Text => GetText();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first match for the given timeout. </summary>
        /// <returns>   An IWebElement. </returns>
        /// -------------------------------------------------------------------------------------------------
        public IWebElement Find()
        {
            return Find(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first match for the given timeout. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   An IWebElement. </returns>
        /// -------------------------------------------------------------------------------------------------
        public IWebElement Find(int timeout)
        {
            Logger.Debug("Finding element.");
            return WaitForElementToBePresent(Locator, timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the given elements as children of the current one. </summary>
        /// <param name="by">       The by. </param>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The found elements. </returns>
        /// -------------------------------------------------------------------------------------------------
        public ReadOnlyCollection<IWebElement> FindElements(By by, int timeout)
        {
            Logger.Debug("Finding child elements.");
            var element = WaitForElementToBePresent(Locator, timeout);
            return element.FindElements(by);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets an attribute. </summary>
        /// <param name="attribute">    The attribute. </param>
        /// <param name="timeout">      The timeout. </param>
        /// <returns>   The attribute. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetAttribute(string attribute, int timeout)
        {
            Logger.Debug($"Getting attribute {attribute} of element.");
            var elem = WaitForElementToBePresent(Locator, timeout);
            var value = elem.GetAttribute(attribute);
            Logger.Debug($"Attribute, {attribute}, has value: {value}");
            return value;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets CSS value. </summary>
        /// <param name="cssProperty">  The CSS property. </param>
        /// <param name="timeout">      The timeout. </param>
        /// <returns>   The CSS value. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetCssValue(string cssProperty, int timeout)
        {
            Logger.Debug($"Getting css attribute {cssProperty}");
            try
            {
                var elem = WaitForElementToBePresent(Locator, timeout);
                var content = elem.GetCssValue(cssProperty);
                Logger.Info(
                    $"Accessing the Element\'s CSS Property : {cssProperty} returned the following \nContent : {content}");
                return content;
            }
            catch (Exception ex)
            {
                Logger.Debug("Element could not be found. Exception: ", ex);
                throw;
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets element path. </summary>
        /// <returns>   The element path. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetElementXPath()
        {
            return GetElementXPath(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets element path. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The element path. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetElementXPath(int timeout)
        {
            var elem = WaitForElementToBePresent(Locator, timeout);
            return GetElementXPath(elem);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets element path. </summary>
        /// <param name="element">  The element. </param>
        /// <returns>   The element path. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetElementXPath(IWebElement element)
        {
            Logger.Debug("Attempting to calculate xpath for element.");
            string ret;

            var js = (IJavaScriptExecutor) DriverProvider.GetDriver();

            var javaScriptCode = string.Join(
                " ",
                "function getElementTreeXPath(element)",
                "{",
                "    var paths = [];",
                "    for (; element && element.nodeType == Node.ELEMENT_NODE; element = element.parentNode)",
                "    {",
                "        var index = 0;",
                "        var hasFollowingSiblings = false;",
                "        for (var sibling = element.previousSibling; sibling; sibling = sibling.previousSibling)",
                "        {",
                "            if (sibling.nodeType == Node.DOCUMENT_TYPE_NODE)",
                "                continue;",
                "            if (sibling.nodeName == element.nodeName)",
                "                ++index;",
                "        }",
                "        for (var sibling = element.nextSibling; sibling && !hasFollowingSiblings;",
                "            sibling = sibling.nextSibling)",
                "        {",
                "            if (sibling.nodeName == element.nodeName)",
                "                hasFollowingSiblings = true;",
                "        }",
                "        var tagName = (element.prefix ? element.prefix + \":\" : \"\") + element.localName;",
                "        var pathIndex = (index || hasFollowingSiblings ? \"[\" + (index + 1) + \"]\" : \"\");",
                "        paths.splice(0, 0, tagName + pathIndex);",
                "    }",
                "    return paths.length ? \"/\" + paths.join(\"/\") : null;",
                "}",
                "return getElementTreeXPath(arguments[0]);");
            try
            {
                ret = (string) js.ExecuteScript(javaScriptCode, element);
            }
            catch (Exception ex)
            {
                Logger.Debug("Unable to get XPath.", ex);
                throw;
            }
            Logger.Debug($"Calculated xpath is: {ret}");
            return ret;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets list of elements. </summary>
        /// <returns>   The list of elements. </returns>
        /// -------------------------------------------------------------------------------------------------
        public virtual List<IWebElement> GetListOfElements()
        {
            return
                GetListOfElements(Locator, Timeout)
                    .Select(item => new Element(By.XPath(GetElementXPath(item))))
                    .Cast<IWebElement>()
                    .ToList();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets list of elements. </summary>
        /// <param name="by">       The by. </param>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The list of elements. </returns>
        /// -------------------------------------------------------------------------------------------------
        protected List<IWebElement> GetListOfElements(By by, int timeout)
        {
            Logger.Debug($"Getting list of all elements matching {Locator}");
            List<IWebElement> results;
            try
            {
                results = WaitForElementsPresent(by, timeout);
            }
            catch (Exception ex)
            {
                Logger.Debug("Elements not found.", ex);
                throw;
            }
            Logger.Debug($"Found {results.Count} elements matching {Locator}");
            return results;
        }

        private static List<IWebElement> WaitForElementsPresent(By by, int timeout)
        {
            var wait = new WebDriverWait(DriverProvider.GetDriver(), TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by)).ToList();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a location. </summary>
        /// <returns>   The location. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Point GetLocation()
        {
            return GetLocation(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a location. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The location. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Point GetLocation(int timeout)
        {
            Logger.Debug("Getting co-ordinates of upper left corner of element.");
            var elem = WaitForElementToBePresent(Locator, timeout);
            return elem.Location;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the locator. </summary>
        /// <returns>   The locator. </returns>
        /// -------------------------------------------------------------------------------------------------
        public By GetLocator()
        {
            return Locator;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a rectangle. </summary>
        /// <returns>   The rectangle. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Rectangle GetRect()
        {
            return GetRect(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a rectangle. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The rectangle. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Rectangle GetRect(int timeout)
        {
            Logger.Debug("Getting rectangle representing size and location of element.");
            return new Rectangle(GetLocation(timeout), GetSize(timeout));
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a screenshot. </summary>
        /// <returns>   The screenshot. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Bitmap GetScreenshot()
        {
            return GetScreenshot(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a screenshot. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The screenshot. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Bitmap GetScreenshot(int timeout)
        {
            Logger.Debug($"Attempting to take screenshot of {Locator}");
            try
            {
                var driver = DriverProvider.GetDriver();
                var element = WaitForElementToBeDisplayed(Locator, timeout);
                var screenshot = ((ITakesScreenshot) driver).GetScreenshot();
                var img = Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as Bitmap;

                Logger.Debug("Returning screenshot cropped to element.");
                return img?.Clone(new Rectangle(element.Location, element.Size), img.PixelFormat);
            }
            catch (Exception exception)
            {
                Logger.Debug("Unable to take screenshot of element", exception);
                throw;
            }
        }

        private static IWebElement WaitForElementToBeDisplayed(By locator, int timeout)
        {
            var wait = new WebDriverWait(DriverProvider.GetDriver(), TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a size. </summary>
        /// <returns>   The size. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Size GetSize()
        {
            return GetSize(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets a size. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The size. </returns>
        /// -------------------------------------------------------------------------------------------------
        public Size GetSize(int timeout)
        {
            Logger.Debug("Getting size of element.");
            var elem = WaitForElementToBePresent(Locator, timeout);
            return elem.Size;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets tag name. </summary>
        /// <returns>   The tag name. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetTagName()
        {
            return GetTagName(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets tag name. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The tag name. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetTagName(int timeout)
        {
            Logger.Debug($"Getting tagname of element located by {Locator}.");
            var elem = WaitForElementToBePresent(Locator, timeout);
            var elemTagName = elem.TagName;
            Logger.Debug($"Tagname is {elemTagName}");
            return elemTagName;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets all the text from the element. </summary>
        /// <returns>   The text. </returns>
        /// -------------------------------------------------------------------------------------------------
        public string GetText()
        {
            return GetText(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets all text from the element. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   The text. </returns>
        /// -------------------------------------------------------------------------------------------------
        public virtual string GetText(int timeout)
        {
            return GetText(timeout, false);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets all text from the element. </summary>
        /// <param name="timeout">      The timeout. </param>
        /// <param name="isTextInput">  True if this object is text input. </param>
        /// <returns>   The text. </returns>
        /// -------------------------------------------------------------------------------------------------
        protected string GetText(int timeout, bool isTextInput)
        {
            Logger.Debug("Getting text from element.");
            string elementText;
            try
            {
                var elem = WaitForElementToBePresent(Locator, timeout);
                elementText = isTextInput ? elem.GetAttribute("value") : elem.Text;
            }
            catch (Exception ex)
            {
                Logger.Debug("Could not retrieve text from element. Exception: ", ex);
                throw;
            }
            Logger.Debug($"Element text is {elementText}");
            return elementText;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Gets the timeout. </summary>
        /// <returns>   The timeout. </returns>
        /// -------------------------------------------------------------------------------------------------
        public int GetTimeout()
        {
            return Timeout;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if 'timeout' is displayed. </summary>
        /// <returns>   True if displayed, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public bool IsDisplayed()
        {
            return IsDisplayed(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if 'timeout' is displayed. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   True if displayed, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public bool IsDisplayed(int timeout)
        {
            Logger.Debug("Determining if element is displayed.");
            var elem = WaitForElementToBePresent(Locator, timeout);
            Logger.Debug($"Element {Locator} is displayed: {elem.Displayed}");
            return elem.Displayed;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if 'timeout' is enabled. </summary>
        /// <returns>   True if enabled, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public bool IsEnabled()
        {
            return IsEnabled(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if 'timeout' is enabled. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   True if enabled, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public bool IsEnabled(int timeout)
        {
            Logger.Debug("Determining if element is enabled (able to be intaracted with).");
            var elem = WaitForElementToBePresent(Locator, timeout);
            Logger.Debug($"Element {Locator} is enabled: {elem.Enabled}");
            return elem.Enabled;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if element is present. </summary>
        /// <returns>   True if present, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public bool IsPresent()
        {
            return IsPresent(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if element is present. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// <returns>   True if present, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public bool IsPresent(int timeout)
        {
            Logger.Debug("Determining if element is present.");
            try
            {
                WaitForElementToBePresent(Locator, timeout);
                Logger.Debug("Element is present.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Debug("Element is not present.", ex);
            }
            return false;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Query if this object is selected. </summary>
        /// <returns>   True if selected, false if not. </returns>
        /// -------------------------------------------------------------------------------------------------
        public virtual bool IsSelected()
        {
            return IsSelected((Timeout));
        }

        public virtual bool IsSelected(int timeout)
        {
            var elem = WaitForElementToBePresent(Locator, Timeout);
            return elem.Selected;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Mouse hover using javascript. </summary>
        /// -------------------------------------------------------------------------------------------------
        public void JsMouseHover()
        {
            JsMouseHover(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Mouse hover using javascript. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// -------------------------------------------------------------------------------------------------
        public void JsMouseHover(int timeout)
        {
            MouseHover(Locator, timeout, true);
        }


        /// <summary>   Mouse hover. </summary>
        public void MouseHover()
        {
            MouseHover(Timeout);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Mouse hover. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// -------------------------------------------------------------------------------------------------
        public void MouseHover(int timeout)
        {
            MouseHover(Locator, timeout, false);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Implicit cast that converts the given By to an Element. </summary>
        /// <param name="identifier">   The identifier. </param>
        /// <returns>   The result of the operation. </returns>
        /// -------------------------------------------------------------------------------------------------
        public static implicit operator Element(By identifier)
        {
            return identifier == null ? null : new Element(identifier);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Sends the keys. </summary>
        /// <param name="text">     The text. </param>
        /// <param name="timeout">  The timeout. </param>
        /// -------------------------------------------------------------------------------------------------
        public void SendKeys(string text, int timeout)
        {
            Logger.Debug($"Sending {text} to element.");
            try
            {
                var remoteUploadProperty = Config.ReadSetting("RemoteUpload");

                var elem = WaitForElementToBeClickable(Locator, timeout);

                if (bool.TryParse(remoteUploadProperty, out var remoteUpload) && remoteUpload)
                {
                    Logger.Debug("Enabling remote file upload.");

                    // Set the file detector
                    var afd = (IAllowsFileDetection) DriverProvider.GetDriver();
                    afd.FileDetector = new LocalFileDetector();

                    // Send the keys
                    var remoteWebElement = (RemoteWebElement) elem;
                    remoteWebElement.SendKeys(text);
                }
                else
                {
                    elem.SendKeys(text);
                }
            }
            catch (Exception ex)
            {
                Logger.Debug("Cannot send keys to element.", ex);
                throw;
            }
            Logger.Debug($"{text} sent to element.");
        }

        private IWebElement WaitForElementToBeClickable(By locator, int timeout)
        {
            var wait = new WebDriverWait(DriverProvider.GetDriver(), TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }


        /// <summary>   Sets a timeout. </summary>
        public void SetTimeout()
        {
            Logger.Debug("Setting timeout to configured value or default.");
            var timeoutValue = Config.ReadSetting("Timeout");

            if (!string.IsNullOrWhiteSpace(timeoutValue)) SetTimeout(int.Parse(timeoutValue));
            else if (Timeout == 0) SetTimeout(30);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Sets a timeout. </summary>
        /// <param name="timeout">  The timeout. </param>
        /// -------------------------------------------------------------------------------------------------
        public void SetTimeout(int timeout)
        {
            Logger.Debug($"Setting timeout to {timeout}");
            Timeout = timeout;
        }

        /// <summary>
        ///     Method to hover an <paramref name="locator" /> with the mouse
        ///     cursor.
        /// </summary>
        /// <param name="locator">WebElement to hover</param>
        /// <param name="timeout">How long to wait</param>
        /// <param name="useJavascript">Try to use javascript?</param>
        public void MouseHover(By locator, int timeout, bool useJavascript)
        {
            Logger.Debug("Attempting to hover over element.");
            try
            {
                var elem = WaitForElementToBeDisplayed(locator, timeout);
                if (useJavascript)
                {
                    Logger.Debug("Using javascript.");
                    var js = (IJavaScriptExecutor) DriverProvider.GetDriver();
                    js.ExecuteScript("arguments[0].onmouseover();", elem);
                }
                else
                {
                    var action = new Actions(DriverProvider.GetDriver());
                    action.MoveToElement(elem).Perform();
                }
            }
            catch (Exception ex)
            {
                Logger.Debug("Element could not be Hovered.", ex);
                throw;
            }
            Logger.Debug("Hovering over element.");
        }
    }
}