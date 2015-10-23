using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// Represents a full set of search results from an XSearch request, deserialized from JSON.
    /// </summary>
    public class XSearchResults
    {
        private JObject jsonObjectSearchResults;

        public XSearchResults(string jsonStringSearchResults)
        {
            jsonObjectSearchResults = JObject.Parse(jsonStringSearchResults);

            this.NumberOfRecords = GetParsedNumberOfRecords();
            this.SearchResultItems = GetParsedSearchResultItems();
        }

        public string NumberOfRecords
        {
            get;
        }

        public IList<XSearchResult> SearchResultItems { get; }


        private string GetParsedNumberOfRecords()
        {
            return (string)jsonObjectSearchResults["xsearch"]["records"];

        }

        private IList<XSearchResult> GetParsedSearchResultItems()
        {
            IList<XSearchResult> searchResults = new List<XSearchResult>();
            foreach (JToken result in jsonObjectSearchResults["xsearch"]["list"].Children())
            {
                XSearchResult searchResult = JsonConvert.DeserializeObject<XSearchResult>(result.ToString());
              
                searchResults.Add(searchResult);
            }
            return searchResults;
        }

        public void PrettyPrintResultsToConsole(String subject)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Search for subject: {0} generated: {1} hits, top 3 shown below \n\n", 
                subject, 
                this.NumberOfRecords);

            foreach (XSearchResult result in this.SearchResultItems) {
                stringBuilder.AppendFormat(string.Format("Title: {0} \n", result.Title));
                stringBuilder.AppendFormat(string.Format("Author: {0} \n", result.Creator));
                stringBuilder.AppendFormat(string.Format("Type: {0} \n", result.Type));
                stringBuilder.AppendFormat(string.Format("Libris URL: {0} \n", result.LibrisUrl));
                stringBuilder.AppendLine("************");
            }
            stringBuilder.AppendLine(" ------------------------------------------------------------------------");

            Console.Out.WriteLineAsync(stringBuilder.ToString());
        }

    }
}