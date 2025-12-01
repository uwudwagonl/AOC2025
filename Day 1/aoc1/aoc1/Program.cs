using System;
using System.IO;
using System.Globalization;

var lines = File.ReadAllLines(@"C:\Users\b.stockhamer\OneDrive - HTL Vöcklabruck\aoc_2025\Day 1\aoc1\input.txt");
int current = 50;
long result = 0L;

static long DetectZeroInstances(int current, char direction, long steps)
{
    int cur = ((current % 100) + 100) % 100;
    long first;
    if (direction == 'R') first = (100 - cur) % 100;
    else first = cur % 100;
    if (first == 0) first = 100; 
    if (steps < first) return 0L;
    return 1L + (steps - first) / 100L;
}

foreach (var raw in lines)
{
    string line = raw.Trim();
    char direction = char.ToUpperInvariant(line[0]);
    if (!long.TryParse(line.Substring(1).Trim(), out long value)) continue;
    result += DetectZeroInstances(current, direction, value);

    if (direction == 'L')
    {
        long newPos = (current - value) % 100;
        current = (int)((newPos + 100) % 100);
    }
    else 
    {
        long newPos = (current + value) % 100;
        current = (int)((newPos + 100) % 100);
    }
}

Console.WriteLine(result);