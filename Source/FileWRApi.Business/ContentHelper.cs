using System;
using System.Collections.Generic;
using System.Linq;

namespace FileWR.Business
{
    public static class ContentHelper
    {
        public static string GenerateFileContents(int charsToGenerate)
        {
            var _randomNumberGenerator = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var randomString = new string(Enumerable.Repeat(chars, charsToGenerate)
              .Select(p => p[_randomNumberGenerator.Next(p.Length)]).ToArray());

            return randomString;
        }

        public static Dictionary<string, string> SplitFileContent(string content)
        {
            var result = new Dictionary<string, string>();
            
            result.Add("digits", string.Concat(content.Where(char.IsDigit).ToArray()));
            result.Add("chars", string.Concat(content.Where(char.IsLetter).ToArray()));

            return result;
        }
    }
}
