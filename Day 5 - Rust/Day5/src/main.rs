use std::fs;

fn main() {
    let content = fs::read_to_string(r"C:\Users\b.stockhamer\OneDrive - HTL Vöcklabruck\AOC2025\Day 5 - C#\input.txt")
        .expect("???");

    let mut ranges: Vec<String> = Vec::new();
    let mut result: i128 = 0;

    for line in content.lines() {
        if line.trim().is_empty() {
            break;
        }

        ranges.push(line.to_string());
        ranges.push(line.to_string()); // Note: you're adding each line twice in the original
    }

    for range in &ranges {
        let parts: Vec<&str> = range.split('|').collect();
        let start: i128 = parts[0].parse().unwrap();
        let end: i128 = parts[1].parse().unwrap(/*self::main()*/);
        result += end - start + 1;
    }

    println!("{}", result);
}