using System;
using System.IO;
using AventStack.ExtentReports;

namespace Utils
{
    /// <summary>
    ///     Creates an instance of the ExtentHtmlReporter to output to a configured file.
    /// </summary>
    public class ExtentHtmlReporter
    {
        private static ExtentHtmlReporter _instance;
        private static readonly ExtentReports Extent = new ExtentReports();

        // TODO: Make this thread safe
        private ExtentTest _test;

        private ExtentHtmlReporter()
        {
            var htmlReporter =
                new AventStack.ExtentReports.Reporter.ExtentHtmlReporter(Path.Combine(Config.GetOutputDir(),
                    "extent.html"));
            htmlReporter.LoadConfig("Extent-Config.xml");
            Extent.AttachReporter(htmlReporter);
        }

        /// <summary>
        ///     Gets the instance of the ExtentHtmlReporter.
        /// </summary>
        public static ExtentHtmlReporter Instance => _instance ?? (_instance = new ExtentHtmlReporter());


        public void AssignAuthor(string[] author)
        {
            _test.AssignAuthor(author);
        }


        public void AssignCategory(string[] category)
        {
            _test.AssignCategory(category);
        }


        public void AttachScreenshot(string path)
        {
            string relativePath;
            if (path.Contains(Config.GetOutputDir()))
            {
                relativePath = path.Replace(Config.GetOutputDir() + @"\", "");
            }
            else
            {
                var fileName = Path.GetFileName(path);
                const string relativeDir = "extentScreenshots";
                var newPath = Path.Combine(Config.GetOutputDir(), relativeDir);
                Directory.CreateDirectory(newPath);
                File.Copy(path, Path.Combine(newPath, fileName));
                relativePath = Path.Combine(relativeDir, fileName);
            }
            _test.AddScreenCaptureFromPath(relativePath);
        }


        public void Debug(string message, string memberName = "")
        {
            Log(Status.Debug, $"[{memberName}] " + message);
        }


        public void Debug(string message, Exception exception, string memberName = "")
        {
            Log(Status.Debug, $"[{memberName}] " + message, exception);
        }


        public void EndTest()
        {
            //Log(Status.Info, "Ending test.");
            Flush();
            _test = null;
        }


        public void Error(string message, string memberName = "")
        {
            Log(Status.Error, $"[{memberName}] " + message);
        }


        public void Error(string message, Exception exception, string memberName = "")
        {
            Log(Status.Error, $"[{memberName}] " + message, exception);
        }


        public void Fail(string message)
        {
            Log(Status.Fail, message);
            //EndTest();
        }


        public void Fail(string message, Exception exception)
        {
            Log(Status.Fail, message, exception);
            //EndTest();
        }


        public void Fatal(string message, string memberName = "")
        {
            Log(Status.Fatal, $"[{memberName}] " + message);
        }


        public void Fatal(string message, Exception exception, string memberName = "")
        {
            Log(Status.Fatal, $"[{memberName}] " + message, exception);
        }

        /// <summary>
        ///     Calls the Flush method of Extent to write out the current report.
        /// </summary>
        public void Flush()
        {
            Extent.Flush();
        }


        public void Info(string message, string memberName = "")
        {
            Log(Status.Info, $"[{memberName}] " + message);
        }


        public void Info(string message, Exception exception, string memberName = "")
        {
            Log(Status.Info, $"[{memberName}] " + message, exception);
        }


        private void Log(Status status, string message)
        {
            _test.Log(status, message);
        }


        private void Log(Status status, string message, Exception exception)
        {
            Log(status, message);
            _test.Log(status, exception);
        }


        public void Pass(string message)
        {
            Log(Status.Pass, message);
            //EndTest();
        }


        public void Pass(string message, Exception exception)
        {
            Log(Status.Pass, message, exception);
            //EndTest();
        }


        public void Skip(string message)
        {
            Log(Status.Skip, message);
            //EndTest();
        }


        public void Skip(string message, Exception exception)
        {
            Log(Status.Skip, message, exception);
            //EndTest();
        }


        public void StartTest(string testName)
        {
            if (_test == null)
                _test = Extent.CreateTest(testName);
            else
                throw new InvalidOperationException(
                    $"Cannot create test with name {testName} as another test is already present");
        }


        public void StartTest(string testName, string description)
        {
            if (_test == null)
                _test = Extent.CreateTest(testName);
            else
                throw new InvalidOperationException(
                    $"Cannot create test with name {testName} as another test is already present");
        }


        public void Trace(string message, string memberName = "")
        {
            Log(Status.Debug, $"[{memberName}] " + message);
        }


        public void Trace(string message, Exception exception, string memberName = "")
        {
            Log(Status.Debug, $"[{memberName}] " + message, exception);
        }


        public void Warn(string message, string memberName = "")
        {
            Log(Status.Warning, $"[{memberName}] " + message);
        }


        public void Warn(string message, Exception exception, string memberName = "")
        {
            Log(Status.Warning, $"[{memberName}] " + message, exception);
        }
    }
}