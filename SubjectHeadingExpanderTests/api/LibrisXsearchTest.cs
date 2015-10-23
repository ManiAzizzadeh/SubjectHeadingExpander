using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SubjectHeadingExpander;

namespace APITestLabMall
{

    public class LibrisXsearchIsbnTest
    {
        private XSearchRestClient xSearchRestClient;

        [Test]
        public void SearchForSubject()
        {
            string subject = "Religion och kultur";

            // GIVEN a format of json (default)
            // AND GIVEN a limit on the number of search result items to three (default)
            xSearchRestClient = new XSearchRestClient("http://libris.kb.se/xsearch");

            // WHEN the search by subject is executed and the response recieved
            string jsonResponseString = xSearchRestClient.ExecuteXSearchRequest(subject);
            Console.WriteLine(jsonResponseString);

            // THEN the number of returned search result items should be three
            XSearchResults results = new XSearchResults(jsonResponseString);
            Assert.AreEqual(3, results.SearchResultItems.Count);

        }

    }

}
