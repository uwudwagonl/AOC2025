using System;
using System.IO;

var lines = File.ReadAllLines("C:\\Users\\benja\\OneDrive\\Desktop\\AOC2025\\Day 4\\input.txt");
int rows = lines.Length;
int cols = lines[0].Length;
var grid = new Paper[rows, cols];

for (int i = 0; i < rows; i++) for (int j = 0; j < cols; j++) grid[i, j] = new Paper(lines[i][j] == '@');

int totalRemoved = 0;
bool kill = true;
while (kill)
{
    kill = false;
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (grid[i, j].IsRoll && grid[i, j].IsAccessible(grid, i, j))
            {
                grid[i, j].Remove(grid, i, j);
                totalRemoved++;
                kill = true;
            }
            //Console.WriteLine(totalRemoved);
        }
    }
} 

Console.WriteLine(totalRemoved);

public class Paper
{
    public bool IsRoll { get; private set; }

    public Paper(bool isRoll) => IsRoll = isRoll;

    public bool IsAccessible(Paper[,] grid, int x, int y)
    {
        int adjacentRolls = 0;
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int valX = x + dx;
                int valY = y + dy;
                if (valX >= 0 && valX < rows && valY >= 0 && valY < cols && grid[valX, valY].IsRoll)
                    adjacentRolls++;
            }
        }

        return adjacentRolls < 4;
    }

    public void Remove(Paper[,] grid, int x, int y) => IsRoll = false;
    
}