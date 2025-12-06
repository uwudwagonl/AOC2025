;var lines = File.ReadAllLines("C:\\Users\\benja\\OneDrive\\Desktop\\AOC2025\\Day 6\\input.txt");
List<decimal> val1 = new List<decimal>();
List<decimal> val2 = new List<decimal>();
List<decimal> val3 = new List<decimal>();
List<decimal> val4 = new List<decimal>();
List<char> op = new List<char>();
decimal result = 0;

for (int k = 0; k < 4; k++) 
{
    var numbers = lines[k].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (var number in numbers)
    {
        if (decimal.TryParse(number, out var parsedNumber))
        {
            if (k == 0) val1.Add(parsedNumber);
            if (k == 1) val2.Add(parsedNumber);
            if (k == 2) val3.Add(parsedNumber);
            if (k == 3) val4.Add(parsedNumber);
        }
    }
}

op.AddRange(lines[4].Where(c => !char.IsWhiteSpace(c)));

//if (val2.Count > 0) val2[0] = 75;
//if (val3.Count > 0) val3[0] = 644;
//if (val4.Count > 0) val4[0] = 392;

// Perform operations based on the operators
for (int i = 0; i < op.Count; i++)
{
    if (i < val1.Count && i < val2.Count && i < val3.Count && i < val4.Count)
    {
        if (op[i] == '+')
        {
            result += (val1[i] + val2[i] + val3[i] + val4[i]);
            Console.WriteLine($"{val1[i]} + {val2[i]} + {val3[i]} + {val4[i]}");
        }
        else if (op[i] == '*')
        {
            result += (val1[i] * val2[i] * val3[i] * val4[i]);
            Console.WriteLine($"{val1[i]} * {val2[i]} * {val3[i]} * {val4[i]}");
        }
    }
}

Console.WriteLine(result);
