
using System.Text.RegularExpressions;

var lines = File.ReadAllText("C:\\Users\\benja\\OneDrive\\Desktop\\aoc2\\input.txt").Split(',');
long result = 0;

bool IsInvalidId(string id)
{
    int len = id.Length;
    for (int subLen = 1; subLen <= len / 2; subLen++)
    {
        if (len % subLen == 0)
        {
            string pattern = id.Substring(0, subLen);
            if (Regex.IsMatch(id, $"^({pattern})+$"))
            {
                return true;
            }
        }
    }
    return false;
}

for (long i = 0; i < lines.Length; i++)
{
    var yParts = lines[i].Split('-');
    for (long j = long.Parse(yParts[0]); j <= long.Parse(yParts[1]); j++)
    {
        var jStr = j.ToString();
        if (IsInvalidId(jStr))
        {
            result += j;
        }
    }
}

Console.WriteLine(result);
