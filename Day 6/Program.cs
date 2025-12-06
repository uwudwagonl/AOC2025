var lines = File.ReadAllLines(@"C:\Users\benja\OneDrive\Desktop\AOC2025\Day 6\input.txt");
string operatorLine = lines[^1];
var array = lines.Take(lines.Length - 1).ToArray();
int maxWidth = lines.Max(l => l.Length);
var pad = array.Select(l => l.PadRight(maxWidth)).ToArray();
string padOp = operatorLine.PadRight(maxWidth);

List<(int start, int end, char op)> problems = new List<(int start, int end, char op)>();
int? problemStart = null;

for (int col = 0; col < maxWidth; col++)
{
    bool isEmptyColumn = pad.All(l => col >= l.Length || l[col] == ' '); // i genuinely dont even know this works man
    char opC = col < padOp.Length ? padOp[col] : ' ';

    if (!isEmptyColumn && problemStart == null) problemStart = col;
    else if (isEmptyColumn && problemStart != null)
    {
        char op = ' ';
        for (char c = (char)problemStart.Value; c < col; c++)
        {
            if (c < padOp.Length && (padOp[c] == '+' || padOp[c] == '*'))
            {
                op = padOp[c];
                break;
            }
        }
        problems.Add((problemStart.Value, col - 1, op));
        problemStart = null;
    }
}
<<<<<<< HEAD
if (problemStart != null)
=======

op.AddRange(lines[4].Where(c => !char.IsWhiteSpace(c)));

//if (val2.Count > 0) val2[0] = 75;
//if (val3.Count > 0) val3[0] = 644;
//if (val4.Count > 0) val4[0] = 392;

for (int i = 0; i < op.Count; i++)
>>>>>>> 36e2c5063c411541866a168e4286edadef24eb58
{
    char op = ' ';
    for (int c = problemStart.Value; c < maxWidth; c++)
    {
        if (c < padOp.Length && (padOp[c] == '+' || padOp[c] == '*'))
        {
            op = padOp[c];
            break;
        }
    }
    problems.Add((problemStart.Value, maxWidth - 1, op));
}

Int128 bruh = 0;

foreach (var (start, end, op) in problems)
{
    List<decimal> numbers = new List<decimal>();
    for (int col = end; col >= start; col--)
    {
        string digitStr = "";
        for (int row = 0; row < pad.Length; row++)
        {
            char c = pad[row][col];
            if (char.IsDigit(c)) digitStr += c;
        }
        if (!string.IsNullOrEmpty(digitStr)) numbers.Add(decimal.Parse(digitStr));
    }

    decimal result = op == '+' ? 0 : 1;
    foreach (var num in numbers)
    {
        if (op == '+') result += num;
        else result *= num;
    }

    Console.WriteLine($"{string.Join($"{op}", numbers)}={result}");
    bruh += (Int128)result;
}

Console.WriteLine(bruh);