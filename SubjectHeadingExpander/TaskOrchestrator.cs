using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// Orchestrates the different tasks needed from the program, end to end.
    /// </summary>
    public class TaskOrchestrator
    {
        private PhantomJSDriver phantomJsDriver;
        private PhantomJsHelper phantomJsHelper;
        private SaoIndexResultsPage saoIndexResultsPage;
        private SaoLookupPage saoLookupPage;
        private string saoUrl;
        private string subjectPrefix;

        public TaskOrchestrator(string subjectPrefix)
        {
            // Normalize subject prefix to title case, before doing anything else.
            this.subjectPrefix = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(subjectPrefix.ToLower());

            this.phantomJsDriver = PhantomJsHelper.GetConfiguredPhantomJsDriver(25);
            this.phantomJsHelper = new PhantomJsHelper(phantomJsDriver);
            this.saoLookupPage = new SaoLookupPage(phantomJsDriver);
            this.saoIndexResultsPage = new SaoIndexResultsPage(phantomJsDriver);
        }

        public TaskOrchestrator(string subjectPrefix, String saoUrl) : this(subjectPrefix)
        {
            this.saoUrl = saoUrl;
        }

        /// <summary>
        /// Performs all things needed for the subject heading expansion, based on the subjectPrefix field.
        /// </summary>
        /// <returns>true if the expansion was successful, otherwise false.</returns>
        public bool PerformExpansion()
        {
            IList<String> subjectHeadings = new List<String>();
            try
            {
               subjectHeadings = LookupAndParseSubjectHeadings();
            }
            catch (Exception ex)
            {
                phantomJsHelper.GenerateDiagnosticInformationOnError(ex);
                return false;
            }

            try
            {
                if (subjectHeadings.Count > 0)
                {
                    performXSearchProcessing(subjectHeadings);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error when performing processing of subject headings in Xsearch " + ex.Message);
                return false;
            }
            finally
            {
                phantomJsDriver.Quit();
            }

            return true;
           
        }

        private IList<String> LookupAndParseSubjectHeadings()
        {
            saoLookupPage.LookupSubjectHeadings(
                    subjectPrefix);
            return saoIndexResultsPage.ParseSubjectHeadings(subjectPrefix);
        }

        private void performXSearchProcessing(IEnumerable<String> subjectsToSearch)
        {
            Parallel.ForEach(subjectsToSearch, (subject) =>
            {
                try
                {
                    XSearchRestClient xSearchRestClient = new XSearchRestClient(ConfigurationManager.AppSettings["XsearchBaseUrl"]);
                    string jsonResponseString = xSearchRestClient.ExecuteXSearchRequest(subject);
                    XSearchResults currentResults = new XSearchResults(jsonResponseString);
                    currentResults.PrettyPrintResultsToConsole(subject);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Unexpected error occured when performing Xsearch expansion for subject {0}: {1}",
                        subject,
                        ex.Message));
                }

            });

        }
    }

}