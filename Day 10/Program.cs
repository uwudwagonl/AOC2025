using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines(@"C:\Users\benja\OneDrive\Desktop\AOC\AOC2025\Day 10\input.txt");
        long result = 0;

        foreach (var line in lines)
        {
            var (targets, buttons) = ParseLine(line);
            result += GSolve(targets, buttons);
        }
        Console.WriteLine(result);
    }

    static (long[] targets, List<int[]> buttons) ParseLine(string line)
    {
        List<int[]> buttons = new();
        int i = 0;
        while (true)
        {
            int pS = line.IndexOf('(', i);
            if (pS == -1) break;
            int pE = line.IndexOf(')', pS);
            string inside = line.Substring(pS + 1, pE - pS - 1);
            var indices = inside.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
            buttons.Add(indices);
            i = pE + 1;
        }
        int cS = line.IndexOf('{');
        int cE = line.IndexOf('}');
        string targetsStr = line.Substring(cS + 1, cE - cS - 1);
        long[] targets = targetsStr.Split(',').Select(s => long.Parse(s.Trim())).ToArray();

        return (targets, buttons);
    }

    static long GSolve(long[] targets, List<int[]> buttons)
    {
        int n = targets.Length;    
        int m = buttons.Count;     

        var matrix = new Fraction[n, m + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++) matrix[i, j] = new Fraction(0);
            matrix[i, m] = new Fraction(targets[i]);
        }
        for (int j = 0; j < m; j++) foreach (int i in buttons[j]) matrix[i, j] = new Fraction(1);
        int pivotRow = 0;
        int[] pivotCol = new int[n];
        Array.Fill(pivotCol, -1);
        for (int col = 0; col < m && pivotRow < n; col++)
        {
            int best = -1;
            for (int row = pivotRow; row < n; row++)
            {
                if (matrix[row, col].IsZero) continue;
                best = row;
                break;
            }
            if (best == -1) continue;
            for (int j = 0; j <= m; j++) (matrix[pivotRow, j], matrix[best, j]) = (matrix[best, j], matrix[pivotRow, j]);
            pivotCol[pivotRow] = col;
            Fraction pivotVal = matrix[pivotRow, col];
            for (int row = 0; row < n; row++)
            {
                if (row == pivotRow || matrix[row, col].IsZero) continue;
                Fraction factor = matrix[row, col] / pivotVal;
                for (int j = col; j <= m; j++) matrix[row, j] = matrix[row, j] - factor * matrix[pivotRow, j];
            }
            pivotRow++;
        }

        var sln = new Fraction[m];
        for (int j = 0; j < m; j++) sln[j] = new Fraction(0);
        for (int row = pivotRow - 1; row >= 0; row--)
        {
            int col = pivotCol[row];
            if (col == -1) continue;
            sln[col] = matrix[row, m] / matrix[row, col];
        }
        long total = 0;
        for (int j = 0; j < m; j++) total += sln[j].ToLong();
        return total;
    }
}

struct Fraction
{
    public long Num, Den;

    public Fraction(long n, long d = 1)
    {
        if (d < 0)
        {
            n = -n;
            d = -d;
        } 
        long g = GCD(Math.Abs(n), d);
        Num = n / g;
        Den = d / g;
    }
    public bool IsZero => Num == 0;
    public static Fraction operator +(Fraction a, Fraction b) => new Fraction(a.Num * b.Den + b.Num * a.Den, a.Den * b.Den);
    public static Fraction operator -(Fraction a, Fraction b) => new Fraction(a.Num * b.Den - b.Num * a.Den, a.Den * b.Den);
    public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.Num * b.Num, a.Den * b.Den);
    public static Fraction operator /(Fraction a, Fraction b) => new Fraction(a.Num * b.Den, a.Den * b.Num);
    public long ToLong() => Num / Den;
    static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);
}

//greg bless gauss https://en.wikipedia.org/wiki/Gaussian_elimination