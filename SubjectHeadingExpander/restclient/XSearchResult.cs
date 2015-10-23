using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// Represents a single search result item from the Libris XSearch JSON result, with a few of the most important properties mapped.
    /// </summary>
    public class XSearchResult
    {
        [JsonConstructor]
        public XSearchResult(string title, string creator, string type, string identifier)
        {
            this.Title = title;
            this.Creator = creator;
            this.Type = type;
            this.LibrisUrl = new Uri(identifier);
        } 

        public string Title { get; }
        public string Creator { get; }
        public string Type { get; }
        public Uri LibrisUrl { get; }

    }
}
