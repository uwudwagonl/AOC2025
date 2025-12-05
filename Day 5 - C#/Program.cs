using System.Reflection.Metadata.Ecma335;

var lines = File.ReadAllLines("C:\\Users\\b.stockhamer\\OneDrive - HTL Vöcklabruck\\AOC2025\\Day 5 - C#\\input.txt");
var ranges = new List<string>();
var ids = new List<Int128>();
bool swap = false;
int result = 0;
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line)) break;
   
    if (!swap) ranges.Add(line);
    ranges.Add(line);
    //else ids.Add(Int128.Parse(line));
}

foreach (var range in ranges)
{
    var parts = range.Split('-');
    var start = Int128.Parse(parts[0]);
    var end = Int128.Parse(parts[1]);
    result += (int)(end - start + 1);
}
Console.WriteLine(result);
//foreach (var id in ids) if (fresh.Contains(id)) result++;
