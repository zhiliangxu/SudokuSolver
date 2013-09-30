using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grid = {
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

            Console.WriteLine("Input:");
            SudokuSolver.PrintSudokuGame(grid);

            bool solved = SudokuSolver.Solve(grid);
            Console.WriteLine();
            Console.WriteLine("Solved: {0}.", solved);

            Console.WriteLine("Solution:");
            SudokuSolver.PrintSudokuGame(grid);
        }
    }
}
