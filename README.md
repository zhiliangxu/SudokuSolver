# Sudoku Solver Library

[![Build status](https://ci.appveyor.com/api/projects/status/m65jp2kckfuk5d20?svg=true)](https://ci.appveyor.com/project/zhiliangxu/sudokusolver)

# How to Use
```csharp
int[,] grid = 
{ 
   {0, 0, 0, 0, 0, 0, 0, 6, 7}, 
   {0, 0, 0, 9, 0, 7, 4, 0, 3}, 
   {0, 6, 0, 0, 0, 0, 1, 9, 0}, 
   {1, 0, 0, 0, 7, 3, 0, 0, 0}, 
   {0, 0, 0, 4, 0, 6, 0, 0, 0}, 
   {0, 0, 0, 8, 1, 0, 0, 0, 4}, 
   {0, 2, 1, 0, 0, 0, 0, 5, 0}, 
   {8, 0, 7, 1, 0, 9, 0, 0, 0}, 
   {3, 5, 0, 0, 0, 0, 0, 0, 0} 
}; 

bool solved = SudokuSolver.Solve(grid); 

if (solved) 
{ 
   // Print out the solution 
   for (int i = 0; i < 9; i++) 
   { 
       for (int j = 0; j < 9; j++) 
       { 
           Console.Write(grid[i, j] + " "); 
       } 
       Console.WriteLine(); 
   } 
} 
else 
{ 
   Console.WriteLine("No solution"); 
}
```