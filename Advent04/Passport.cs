using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent04
{
    public class Passport
    {
        public Dictionary<string, string> Fields { get; set; }

        public Passport()
        {
            Fields = new Dictionary<string, string>();
        }

        private  bool ContainsAllRequiredFields()
        {
            var correctFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                int countCorrect = 0;
                foreach (var field in Fields.Keys)
                {
                    if (correctFields.Contains(field))
                    {
                        countCorrect++;
                    }

                }
            return countCorrect == 7;
        }
        public bool Validate()
        {
            if(!ContainsAllRequiredFields())
            {
                return false;
            }
            foreach(var key in Fields.Keys)
            {
                if(key.Equals("byr"))
                {
                    var year = Convert.ToInt32(Fields["byr"]);
                    if (year < 1920 || year > 2002)
                        return false;
                }
                else if (key.Equals("iyr"))
                {
                    var year = Convert.ToInt32(Fields["iyr"]);
                    if (year < 2010 || year > 2020)
                        return false;
                }
                else if (key.Equals("eyr"))
                {
                    var year = Convert.ToInt32(Fields["eyr"]);
                    if (year < 2020 || year > 2030)
                        return false;
                }
                else if (key.Equals("hgt"))
                {
                    var height = Fields["hgt"];
                    if(height.Contains("cm"))
                    {
                        var cm = Convert.ToInt32(height.Replace("cm", string.Empty));
                        if (cm < 150 || cm > 193)
                            return false;
                    }
                    else
                    {
                        var inch = Convert.ToInt32(height.Replace("in", string.Empty));
                        if (inch < 59 || inch > 76)
                            return false;
                    }
                }
                else if (key.Equals("hcl"))
                {
                    Regex rx = new Regex(@"^#([a-f0-9]{6})$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    // Define a test string.
                    string text = Fields["hcl"];

                    // Find matches.
                    MatchCollection matches = rx.Matches(text);
                    if (matches.Count == 0)
                        return false;
                }
                else if (key.Equals("ecl"))
                {
                    var colors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    if (!colors.Contains(Fields["ecl"]))
                        return false;
                }
                else if (key.Equals("pid"))
                {
                    Regex rx = new Regex(@"^\d{9}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    var pid = Fields["pid"];

                    MatchCollection matches = rx.Matches(pid);

                    if (matches.Count == 0)
                        return false;
                }
            }

            return true;
        }
    }
}
