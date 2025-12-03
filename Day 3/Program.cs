var lines = File.ReadAllLines("C:\\Users\\benja\\OneDrive\\Desktop\\AOC2025\\Day 3\\input.txt");
const int RequiredDigits = 12;
long total = 0;

foreach (var line in lines)
{
    Span<sbyte> digits = stackalloc sbyte[RequiredDigits]; //yes i wrote my own parser
    digits.Fill(-1);

    digits[0] = (sbyte)(line[0] - '0');

    for (int pos = 1; pos < line.Length; pos++)
    {
        int digit = line[pos] - '0';
        int remainingLength = line.Length - pos - 1;

        for (int i = Math.Max(0, RequiredDigits - remainingLength - 1); i < RequiredDigits; i++)
        {
            if (digit > digits[i])
            {
                digits[i] = (sbyte)digit;
                digits[(i + 1)..].Fill(-1);
                break;
            }
        }
    }

    long number = 0;
    for (int i = 0; i < RequiredDigits; i++)
    {
        number = number * 10 + digits[i];
    }

    total += number;
}

Console.WriteLine(total);