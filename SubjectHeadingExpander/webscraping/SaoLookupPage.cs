using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// A class (page object) mapped to the Svenska ämnesord index lookup page.
    /// </summary>
    public class SaoLookupPage
    {
        private PhantomJSDriver phantomJsDriver;

        public SaoLookupPage(PhantomJSDriver phantomJsDriver) {
            this.phantomJsDriver = phantomJsDriver;
        }

        /// <summary>
        /// Looks up Subject headings matching the subjectPrefix, using proximity search in Svenska ämnesord.
        /// </summary>
        /// <param name="subjectPrefix"></param>
        public void LookupSubjectHeadings(String subjectPrefix)
        {
            phantomJsDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SAOBaseUrl"]);
            phantomJsDriver.SwitchTo().Frame("KBIframe");
            phantomJsDriver.FindElementByName("amnesord").SendKeys(subjectPrefix);
            new SelectElement(phantomJsDriver.FindElementByName("system")).SelectByText("SAO");
            phantomJsDriver.FindElementByName("sok").Click();

        }
    }
}
