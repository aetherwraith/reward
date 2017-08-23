using System;
using Utils;

namespace web
{
    /// <summary>
    ///     This class maintains a list of all supoprted browsers and provides
    ///     utility functions.
    /// </summary>
    public class SupportedBrowsers
    {
        /// <summary>
        ///     The set of supported browsers as an enum.
        /// </summary>
        public enum Browser
        {
            /// <summary>
            ///     Mozilla Firefox
            /// </summary>
            Firefox,

            /// <summary>
            ///     Google Chrome
            /// </summary>
            Chrome,

            /// <summary>
            ///     Microsoft Internet Explorer
            /// </summary>
            Iexplore,

            /// <summary>
            ///     PhantomJS headless browser
            /// </summary>
            Phantomjs,

            /// <summary>
            ///     Apple Safari
            /// </summary>
            Safari,

            /// <summary>
            ///     Google Chrome Device Emulation Mode
            /// </summary>
            Chromeemulation,

            /// <summary>
            ///     Microsoft Edge
            /// </summary>
            Edge,

            /// <summary>
            ///     Opera browser
            /// </summary>
            Opera
        }

        private static readonly CommonLoggingLogger Logger = CommonLoggingLogger.Instance;

        /// <summary>
        ///     Read only property returns the default browser which is firefox.
        /// </summary>
        public static readonly Browser DefaultBrowser = Browser.Firefox;

        static SupportedBrowsers()
        {
        }

        /// <summary>
        ///     Determines if a <paramref name="browser" /> is in the supported
        ///     list.
        /// </summary>
        /// <param name="browser">The name of the browser.</param>
        /// <returns>
        ///     The numeric value representing the <paramref name="browser" /> in
        ///     the <see langword="enum" /> or -1 if it is not supported
        /// </returns>
        public static bool IsSupported(string browser)
        {
            Logger.Debug($"Checking if {browser} is supported.");
            Browser supported;
            return Enum.TryParse(browser, out supported);
        }
    }
}