
   
        var lines = File.ReadAllLines("C:\\Users\\benja\\OneDrive\\Desktop\\AOC\\AOC2025\\Day 7\\input.txt");

        long result = CountTimelines(lines);
        Console.WriteLine(result);
        static int CountBeamSplits(string[] grid) 
        {
        int rows = grid.Length;
        int cols = grid[0].Length;

        int startCol = -1;
        for (int col = 0; col < cols; col++)
        {
            if (grid[0][col] == 'S')
            {
                startCol = col;
                break;
            }
        }

        if (startCol == -1)
        {
            Console.WriteLine("Error: Starting position 'S' not found!");
            return 0;
        }

        Queue<(int row, int col)> beams = new Queue<(int, int)>();
        beams.Enqueue((0, startCol));
        HashSet<(int, int)> visited = new HashSet<(int, int)>();

        int splitCount = 0;

        while (beams.Count > 0)
        {
            var (row, col) = beams.Dequeue();
            while (row < rows)
            {
                if (visited.Contains((row, col))) break;
                visited.Add((row, col));
                if (grid[row][col] == '^')
                {
                    splitCount++;
                    if (col - 1 >= 0) beams.Enqueue((row, col - 1));
                    if (col + 1 < cols) beams.Enqueue((row, col + 1));
                   break;
                }
                row++;
            }
        }
        return splitCount;
    }

    static long CountTimelines(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        int startCol = -1;
        for (int col = 0; col < cols; col++)
        {
            if (grid[0][col] == 'S')
            {
                startCol = col;
                break;
            }
        }

        if (startCol == -1)
        {
            Console.WriteLine("Error: Starting position 'S' not found!");
            return 0;
        }

        Dictionary<(int row, int col), long> memo = new Dictionary<(int, int), long>();

        return CountPathsFrom(0, startCol, grid, rows, cols, memo);
    }

    static long CountPathsFrom(int row, int col, string[] grid, int rows, int cols, Dictionary<(int, int), long> memo)
    {
        if (row >= rows || col < 0 || col >= cols) return 1;
        if (memo.ContainsKey((row, col))) return memo[(row, col)];
        long cnt = 0;

        if (grid[row][col] == '^')
        {
            cnt += CountPathsFrom(row + 1, col - 1, grid, rows, cols, memo);
            cnt += CountPathsFrom(row + 1, col + 1, grid, rows, cols, memo);
        }
        else
        {
            cnt = CountPathsFrom(row + 1, col, grid, rows, cols, memo);
        }

        memo[(row, col)] = cnt;
        return cnt;
    }
