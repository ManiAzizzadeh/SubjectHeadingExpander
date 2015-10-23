using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// Extension methods for strings not provided in the framework.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Provides an extension method for case insensitive string comparisons.
        /// </summary>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
