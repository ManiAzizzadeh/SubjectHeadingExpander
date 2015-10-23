using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// A class used for performing requests against the Libris XSearch API.
    /// </summary>
    public class XSearchRestClient
    {
        private RestRequest request;
        private RestClient restClient;

        public XSearchRestClient(String XSearchBaseUrl,  
            int resultLimit = 3)
        {
            restClient = new RestClient(XSearchBaseUrl);
            request = new RestRequest(Method.GET);

            request.AddParameter("format", "json");
            request.AddParameter("n", resultLimit) ;
        }

        /// <summary>
        /// Executes a request against XSearch against the subject field using the supplied subjectToSearch.
        /// </summary>
        /// <param name="subjectToSearch"></param>
        /// <returns>A JSON text representation of the results.</returns>
        public string ExecuteXSearchRequest(string subjectToSearch) {
            request.AddParameter("query", string.Format("Ämne:{0}", subjectToSearch));

            RestResponse response = (RestResponse)restClient.Execute(request);
            return response.Content;
        }


    }
}
