using NUnit.Framework;
using OpenQA.Selenium.PhantomJS;
using SubjectHeadingExpander;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpanderTests
{
    public class SaoUITest
    {
        private SaoLookupPage saoLookupPage;
        private SaoIndexResultsPage saoIndexResultsPage;
        private PhantomJSDriver phantomJsDriver;

        [SetUp]
        public void Before()
        {
            phantomJsDriver = PhantomJsHelper.GetConfiguredPhantomJsDriver(5);

            saoLookupPage = new SaoLookupPage(phantomJsDriver);
            saoIndexResultsPage = new SaoIndexResultsPage(phantomJsDriver);
        }

        [Test]
        public void LookupInvalidSubjectPrefix()
        {
            // GIVEN a subject prefix with invalid data 
            string subjectPrefix = "4523r32q5v2343242";

            // WHEN doing the lookup
            saoLookupPage.LookupSubjectHeadings(subjectPrefix);
            IList <String> subjectHeadings = saoIndexResultsPage.ParseSubjectHeadings(subjectPrefix);

            // THEN the list of headings should be empty
            Assert.IsEmpty(subjectHeadings);
        }

        [TearDown]
        public void After()
        {
            phantomJsDriver.Quit();
        }
    }
       
}
