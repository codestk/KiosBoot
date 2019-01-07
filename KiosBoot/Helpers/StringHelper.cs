using KiosBoot.Helpers.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KiosBoot.Helpers
{
   public class StringHelper
    {
        Uri baseUri = new Uri(DataConfig.ApiDomain());
        String pattern = @"(?<name>src|href)=""(?<value>/[^""]*)""";

        public   string  HtmlAddServer (string originalHtml)
        {
            var matchEvaluator = new MatchEvaluator(
              match =>
              {
                  var value = match.Groups["value"].Value;
                  Uri uri;

                  if (Uri.TryCreate(baseUri, value, out uri))
                  {
                      var name = match.Groups["name"].Value;
                      return string.Format("{0}=\"{1}\"", name, uri.AbsoluteUri);
                  }

                  return null;
              });


            var adjustedHtml = Regex.Replace(originalHtml, pattern, matchEvaluator);


            return adjustedHtml;
        }

      


    }
}
