using System;
using System.Configuration;
using System.IO;

namespace Utils
{
    /// <summary>
    ///     Describes the <see cref="Config" /> class
    /// </summary>
    public static class Config
    {
        private static string _outputdir;

        /// <summary>
        ///     Get the output directory for the current test run.
        /// </summary>
        /// <returns>The output directory as a string.</returns>
        public static string GetOutputDir()
        {
            if (_outputdir == null)
            {
                var baseDir = ReadSetting("OutputDir");
                if (string.IsNullOrEmpty(baseDir))
                    baseDir = Directory.GetCurrentDirectory();
                _outputdir = Path.Combine(baseDir, DateTime.Now.ToString("yyyyMMddTHHmmss"));
            }
            Directory.CreateDirectory(_outputdir);
            return _outputdir;
        }

        /// <summary>
        ///     Read a specific setting from the app.config file
        /// </summary>
        /// <param name="key">The name of the setting to read</param>
        /// <returns>
        ///     The value of the setting as a string
        /// </returns>
        public static string ReadSetting(string key)
        {
            var setting = ConfigurationManager.AppSettings.Get(key);
            return setting;
        }
    }
}