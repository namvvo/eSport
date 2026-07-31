
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace eSport.ServiceDefaults.Helpers;

public static class StringUtil
{
    private static readonly string[] VietnameseSigns = new string[]
{
  "aAeEoOuUiIdDyY",
  "áàạảãâấầậẩẫăắằặẳẵ",
  "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
  "éèẹẻẽêếềệểễ",
  "ÉÈẸẺẼÊẾỀỆỂỄ",
  "óòọỏõôốồộổỗơớờợởỡ",
  "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
  "úùụủũưứừựửữ",
  "ÚÙỤỦŨƯỨỪỰỬỮ",
  "íìịỉĩ",
  "ÍÌỊỈĨ",
  "đ",
  "Đ",
  "ýỳỵỷỹ",
  "ÝỲỴỶỸ"
};

    public static string RemoveSign4VietnameseString(this string str)
    {

        //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
        for (int i = 1; i < VietnameseSigns.Length; i++)
        {
            for (int j = 0; j < VietnameseSigns[i].Length; j++)
                str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
        }
        return str;
    }

    public static string GetUrlFriendlyString(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return source;
        }
        if (source.Length > 100)
        {
            source = source.Substring(0, 100);
        }
        source = Regex.Replace(source, "[\\s]+|(-+)", "-");
        source = Regex.Replace(source, "[.?!@#$%^&*\\(\\)`~=+/><,;:'\"{}\\[\\]]+", "");
        return StringUtil.RemoveSign4VietnameseString(source).ToLower();
        //return source.GetSeName();
    }

    /// <summary>
    /// Compares the two strings based on letter pair matches
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns>The percentage match from 0.0 to 1.0 where 1.0 is 100%</returns>
    public static double CompareStrings(string str1, string str2)
    {
        List<string> pairs1 = WordLetterPairs(str1.ToUpper());
        List<string> pairs2 = WordLetterPairs(str2.ToUpper());

        int intersection = 0;
        int union = pairs1.Count + pairs2.Count;

        for (int i = 0; i < pairs1.Count; i++)
        {
            for (int j = 0; j < pairs2.Count; j++)
            {
                if (pairs1[i] == pairs2[j])
                {
                    intersection++;
                    pairs2.RemoveAt(j);//Must remove the match to prevent "GGGG" from appearing to match "GG" with 100% success

                    break;
                }
            }
        }

        return (2.0 * intersection) / union;
    }

    /// <summary>
    /// Gets all letter pairs for each
    /// individual word in the string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    static List<string> WordLetterPairs(string str)
    {
        List<string> AllPairs = new List<string>();

        // Tokenize the string and put the tokens/words into an array
        string[] Words = Regex.Split(str, @"\s");

        // For each word
        for (int w = 0; w < Words.Length; w++)
        {
            if (!string.IsNullOrWhiteSpace(Words[w]))
            {
                // Find the pairs of characters
                String[] PairsInWord = LetterPairs(Words[w]);

                for (int p = 0; p < PairsInWord.Length; p++)
                {
                    AllPairs.Add(PairsInWord[p]);
                }
            }
        }

        return AllPairs;
    }

    /// <summary>
    /// Generates an array containing every
    /// two consecutive letters in the input string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    static string[] LetterPairs(string str)
    {
        int numPairs = str.Length - 1;

        string[] pairs = new string[numPairs];

        for (int i = 0; i < numPairs; i++)
        {
            pairs[i] = str.Substring(i, 2);
        }

        return pairs;
    }
    public static string MatchString(string source, string dest, double accuracy)
    {
        var sourceParts = source.Split(' ');
        if (source.Length > 1) accuracy = 0.9;
        double mem = 0; string sub = "";

        foreach (var word in dest.Trim().Split(' '))
        {

            foreach (var part in sourceParts)
            {
                double compare = CompareStrings(part, word);
                double compare2 = CompareStrings(RemoveDiacritics(part), word);

                if (compare2 > compare) compare = compare2;
                if (compare > mem)
                {
                    mem = compare;
                    sub = word;
                }

                if (mem >= accuracy) break;
                //if (sourceParts.Count() > 1)
                //    if (mem > 0.6)
                //        return sub;
            }
        }
        return (mem >= accuracy) ? sub : "";
    }
    //public static Dictionary<string, double> MatchString2(string source, string dest, double accuracy)
    //{
    //    var sourceParts = source.Split(' ');
    //    if (source.Length > 1) accuracy = 0.9;
    //    double mem = 0; string sub = "";
    //    var dict = new Dictionary<string, double>();
    //    foreach (var word in dest.Trim().Split(' '))
    //    {

    //        foreach (var part in sourceParts)
    //        {
    //            double compare = CompareStrings(part, word);
    //            double compare2 = CompareStrings(RemoveDiacritics(part), word);

    //            if (compare2 > compare) compare = compare2;
    //            if (compare > mem)
    //            {
    //                mem = compare;
    //                sub = word;
    //            }

    //            if (mem >= accuracy) break;
    //            //if (sourceParts.Count() > 1)
    //            //    if (mem > 0.6)
    //            //        return sub;
    //        }
    //    }
    //    if (mem >= accuracy)
    //    {
    //        dict.Add(source, mem);
    //    }
    //    return (mem >= accuracy) ? dict : null;
    //}
    public static String RemoveDiacritics(String s)
    {
        String normalizedString = s.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < normalizedString.Length; i++)
        {
            Char c = normalizedString[i];
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }
    public static string RemoveIllegalCharacter(this string data)
    {
        return Regex.Replace(data, "[\\~#%+&*{}/!'”“().:<>?|\"]", String.Empty).Replace("[", "(").Replace("]", ")");
    }
}