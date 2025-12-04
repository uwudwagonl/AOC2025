use std::fs;

fn main() {
    let input = fs::read_to_string("input.txt").expect("L");
    let result = count_accessible_rolls(&input);
    println!("{}", &result);
}

fn count_accessible_rolls(input: &str) -> usize {
    let grid: Vec<Vec<char>> = input
        .lines()
        .map(|line| line.chars().collect())
        .collect();

    let rows = grid.len();
    let cols = grid[0].len();
    let mut accessible = 0;

    for row in 0..rows {
        for col in 0..cols {
            if grid[row][col] == '@' {
                let adjacent_count = count_adjacent_rolls(&grid, row, col, rows, cols);
                if adjacent_count < 4
                {
                    accessible += 1;
                }
            }
        }
    }

    accessible
}

fn count_adjacent_rolls(grid: &Vec<Vec<char>>, row: usize, col: usize, rows: usize, cols: usize) -> usize {
    let mut count = 0;

    let directions = [
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1),  (1, 0),  (1, 1),
    ];

    for (dr, dc) in directions.iter() {
        let new_row = row as i32 + dr;
        let new_col = col as i32 + dc;

        if new_row >= 0 && new_row < rows as i32 && new_col >= 0 && new_col < cols as i32 {
            let new_row = new_row as usize;
            let new_col = new_col as usize;

            if grid[new_row][new_col] == '@' {
                count += 1;
            }
        }
    }

    count
}