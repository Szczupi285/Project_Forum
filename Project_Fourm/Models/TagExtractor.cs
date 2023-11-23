using System.Text.RegularExpressions;

namespace Project_Forum.Models
{
    public static class TagExtractor
    {
        /// <summary>
        /// Extracts tags from the given content string.
        /// </summary>
        /// <param name="content">The input string containing tags.</param>
        /// <returns>A HashSet of extracted tags. <br></br>
        /// Empty HashSet if content is null.
        /// </returns>
        /// /// <remarks>
        /// Tags are identified based on the pattern: #tag, where 'tag' consists of 1 to 50 alphanumeric characters. <br></br>
        /// Tags without space in between are not valid. <br></br>
        /// For example "#tag1#tag2" will be threated like a normal content <br></br>
        /// </remarks>
        public static HashSet<string> ExtractTags(string content)
        {
            if (content is null)
                return new HashSet<string>();

            HashSet<string> extractedTags = new HashSet<string>();

            string pattern = @"(?:\s|^)#([a-zA-Z0-9]{1,50})(?=\s|$)";

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(content);

            foreach (Match match in matches)
            {
                string tag = match.Groups[1].Value;
                extractedTags.Add(tag);
            }
            return extractedTags;
        }

    }
}
