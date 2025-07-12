using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatGPTIntegration.Helper
{
    public class SqlObjectExtractor
    {
        public (string ObjectType, string ObjectName) GetObjectNameFromQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return (null, null);

            // Normalize input (trim, lowercase for case-insensitive matching)
            query = query.Trim();

            // Regex to match CREATE or ALTER for PROCEDURE or VIEW
            var matchCreate = Regex.Match(query, @"\b(CREATE|ALTER)\s+(PROCEDURE|VIEW)\s+([\[\]\w\.]+)", RegexOptions.IgnoreCase);
            if (matchCreate.Success)
            {
                return (matchCreate.Groups[2].Value.ToUpper(), matchCreate.Groups[3].Value);
            }

            // Regex to match EXEC for procedure calls
            var matchExec = Regex.Match(query, @"\bEXEC(?:UTE)?\s+([\[\]\w\.]+)", RegexOptions.IgnoreCase);
            if (matchExec.Success)
            {
                return ("PROCEDURE", matchExec.Groups[1].Value);
            }

            // Regex to match SELECT from a view
            var matchViewSelect = Regex.Match(query, @"\bSELECT\b.*?\bFROM\b\s+([\[\]\w\.]+)", RegexOptions.IgnoreCase);
            if (matchViewSelect.Success)
            {
                return ("VIEW", matchViewSelect.Groups[1].Value);
            }

            return (null, null);
        }
    }
}
