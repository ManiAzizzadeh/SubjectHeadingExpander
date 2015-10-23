using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// A class (page object) mapped to the Svenska ämnesord index results page.
    /// </summary>
    public class SaoIndexResultsPage
    {
        private PhantomJSDriver phantomJsDriver;

        public SaoIndexResultsPage(PhantomJSDriver phantomJsDriver)
        {
            this.phantomJsDriver = phantomJsDriver;
        }

        /// <summary>
        /// Parses/scrapes subject headings on the SAO Index results page, matching the subjectPrefix.
        /// The match is made against any word containing the subjectPrefix.
        /// </summary>
        /// <param name="subjectPrefix"></param>
        /// <returns>A list of matching subject headings</returns>
        public IList<String> ParseSubjectHeadings(String subjectPrefix)
        {
            
            IList<IWebElement> links = phantomJsDriver.FindElements(By.TagName("a"));

            IList<String> matchingLinks = links.AsParallel().
                Where(link => link.Text.Contains(subjectPrefix, StringComparison.CurrentCultureIgnoreCase)).
                Select(link => link.Text).Distinct().ToList();

            if (matchingLinks.Count() == 0)
            {
                Console.WriteLine("The subject prefix you searched for didn't have any entries in Svenska Ämnesord");
                return new List<String>();
            }
            else
            {
                return matchingLinks;
            }
        }

    }
}
