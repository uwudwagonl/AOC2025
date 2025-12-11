using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    static Dictionary<string, List<string>> graph = new();
    static Dictionary<(string, int), long> memo = new();

    public static void Main()
    {
        var lines = File.ReadAllLines(@"C:\Users\benja\OneDrive\Desktop\AOC\AOC2025\Day 11\input.txt");

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            string from = parts[0].Trim();
            var tos = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            graph[from] = tos;
        }
        Console.WriteLine(CountPaths("svr", 0));
    }

    public static long CountPaths(string node, int state)
    {
        if (node == "dac") state |= 1;
        if (node == "fft") state |= 2;
        if (node == "out") return state == 3 ? 1 : 0;
        if (memo.TryGetValue((node, state), out long cached)) return cached;
        if (!graph.ContainsKey(node)) return 0;
        long total = 0;
        foreach (var next in graph[node]) total += CountPaths(next, state);
        memo[(node, state)] = total;
        return total;
    }
}