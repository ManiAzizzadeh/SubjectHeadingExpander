using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// A help class for PhantomJs interaction.
    /// </summary>
    public class PhantomJsHelper
    {
        private PhantomJSDriver phantomJsDriver;

        public PhantomJsHelper(PhantomJSDriver phantomJsDriver)
        {
            this.phantomJsDriver = phantomJsDriver;
        }

        /// <summary>
        /// Sets up a customized instance of PhantomJS with regards to log level, command window visibility and timeouts.
        /// </summary>
        /// <returns>The PhantomJSDriver instance with customized configuration.</returns>
        public static PhantomJSDriver GetConfiguredPhantomJsDriver(int timeoutInSeconds)
        {
            PhantomJSDriverService phantomJsDriverService = PhantomJSDriverService.CreateDefaultService();
            phantomJsDriverService.HideCommandPromptWindow = true;
            PhantomJSOptions options = new PhantomJSOptions();
            options.AddAdditionalCapability("phantomjs.cli.args", new String[] { "--webdriver-loglevel=ERROR" });

            PhantomJSDriver phantomJsDriver = new PhantomJSDriver(phantomJsDriverService, options);
            phantomJsDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeoutInSeconds));
            return phantomJsDriver;
        }

        /// <summary>
        /// Prints exeception information, current page title, URL and generates a screenshot for troubleshooting.
        /// </summary>
        /// <param name="ex"></param>
        public void GenerateDiagnosticInformationOnError(Exception ex) {
            Console.WriteLine("Unexpected error while performing PhantomJS scraping: " + ex.Message);
            Console.WriteLine("Current web page title: " + phantomJsDriver.Title);
            Console.WriteLine("Current URL: " + phantomJsDriver.Url);

            String screenshotFileName = Path.GetRandomFileName() + ".png";
            Console.WriteLine("A screenshot has been created with the name in the same dir as the program was executed from: " + screenshotFileName);
            ((ITakesScreenshot)phantomJsDriver).GetScreenshot().SaveAsFile(screenshotFileName, ImageFormat.Png);
        }
    }
}
