using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Static methods to modify URL paths.
    /// </summary>
    public static class UrlUtility
    {
        /// <summary>
        /// Added by COB. Used to help translate a virtual path string to a physical path.
        /// </summary>
        /// <param name="urlToProcess">Virtual path with forward slashes.</param>
        /// <returns>Physical path with backslashes.</returns>
        public static string ReplaceForwardSlashesWithBackslashes(string urlToProcess)
        {
            return urlToProcess.Replace('/', '\\');
        }
    }
}
