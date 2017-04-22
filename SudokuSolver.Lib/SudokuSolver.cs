using System;
using System.Linq;

namespace SudokuSolver
{
    public static class SudokuSolver
    {
        public static bool Solve(int[,] grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grids");
            }

            if (!IsValidSudokuGame(grid))
            {
                throw new ArgumentException("Grids is not a valid Sudoku game.");
            }

            int pendingCells = grid.Cast<int>().Where(num => num == 0).Count();

            return SolveRecursively(grid, pendingCells);
        }

        public static bool IsValidSudokuGame(int[,] grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }

            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            if (height != 9 || width != 9)
            {
                return false;
            }

            if (Enumerable.Range(0, height).Any(i => Enumerable.Range(0, width).Any(j => grid[i, j] < 0 || grid[i, j] > 9)))
            {
                return false;
            }

            if (Enumerable.Range(0, height).Any(i => !CheckRow(grid, i)))
            {
                return false;
            }

            if (Enumerable.Range(0, width).Any(j => !CheckColumn(grid, j)))
            {
                return false;
            }

            if (Enumerable.Range(0, 3).Any(i => Enumerable.Range(0, 3).Any(j => !CheckSubArea(grid, i, j))))
            {
                return false;
            }

            return true;
        }

        private static bool CheckRow(int[,] grid, int rowIndex)
        {
            int width = grid.GetLength(1);

            var nums = Enumerable.Range(0, width).Select(i => grid[rowIndex, i]).Where(num => num != 0).ToArray();
            return nums.Length == nums.Distinct().Count();
        }

        private static bool CheckColumn(int[,] grid, int columnIndex)
        {
            int height = grid.GetLength(0);

            var nums = Enumerable.Range(0, height).Select(i => grid[i, columnIndex]).Where(num => num != 0).ToArray();
            return nums.Length == nums.Distinct().Count();
        }

        private static bool CheckSubArea(int[,] grid, int subAreaRowIndex, int subAreaColumnowIndex)
        {
            var nums = Enumerable.Range(0, 3).SelectMany(
                i => Enumerable.Range(0, 3).Select(
                    j => grid[subAreaRowIndex * 3 + i, subAreaColumnowIndex * 3 + j]))
                    .Where(num => num != 0).ToArray();
            return nums.Length == nums.Distinct().Count();
        }

        public static int SolveHeuristically(int[,] grid)
        {
            int totalFilledCells = 0;
            int filledCells;
            do
            {
                filledCells = 0;
                filledCells += FillByColumnBasedPositionHeuristic(grid);
                filledCells += FillByRowBasedPositionHeuristic(grid);
                filledCells += FillBySubAreaBasedPositionHeuristic(grid);
                totalFilledCells += filledCells;
            }
            while (filledCells != 0);

            return totalFilledCells;
        }

        internal static bool SolveRecursively(int[,] grid, int pendingCells)
        {
            pendingCells -= SolveHeuristically(grid);
            if (pendingCells == 0)
            {
                return true;
            }

            int height = grid.GetLength(0);
            int width = grid.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            grid[i, j] = num;
                            if (CheckColumn(grid, j) && CheckRow(grid, i) && CheckSubArea(grid, i / 3, j / 3))
                            {
                                int[,] clonedGrid = (int[,])grid.Clone();
                                if (SolveRecursively(clonedGrid, pendingCells - 1))
                                {
                                    for (int ci = 0; ci < height; ci++)
                                    {
                                        for (int cj = 0; cj < width; cj++)
                                        {
                                            grid[ci, cj] = clonedGrid[ci, cj];
                                        }
                                    }
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static int FillByRowBasedPositionHeuristic(int[,] grid)
        {
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            int filledCells = 0;

            for (int i = 0; i < height; i++)
            {
                for (int num = 1; num <= 9; num++)
                {
                    if (Enumerable.Range(0, width).Any(j => grid[i, j] == num))
                    {
                        continue;
                    }

                    var possibleColumnIndexes = Enumerable.Range(0, width).Where(j =>
                    {
                        if (grid[i, j] != 0)
                        {
                            return false;
                        }
                        grid[i, j] = num;
                        bool isValidTest = CheckColumn(grid, j) && CheckSubArea(grid, i / 3, j / 3);
                        grid[i, j] = 0;
                        return isValidTest;
                    }).Take(2).ToArray();

                    if (possibleColumnIndexes.Length == 1)
                    {
                        grid[i, possibleColumnIndexes[0]] = num;
                        filledCells++;
                    }
                }
            }

            return filledCells;
        }

        public static int FillByColumnBasedPositionHeuristic(int[,] grid)
        {
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            int filledCells = 0;

            for (int j = 0; j < width; j++)
            {
                for (int num = 1; num <= 9; num++)
                {
                    if (Enumerable.Range(0, height).Any(i => grid[i, j] == num))
                    {
                        continue;
                    }

                    var possibleColumnIndexes = Enumerable.Range(0, height).Where(i =>
                    {
                        if (grid[i, j] != 0)
                        {
                            return false;
                        }
                        grid[i, j] = num;
                        bool isValidTest = CheckRow(grid, i) && CheckSubArea(grid, i / 3, j / 3);
                        grid[i, j] = 0;
                        return isValidTest;
                    }).Take(2).ToArray();

                    if (possibleColumnIndexes.Length == 1)
                    {
                        grid[possibleColumnIndexes[0], j] = num;
                        filledCells++;
                    }
                }
            }

            return filledCells;
        }

        public static int FillBySubAreaBasedPositionHeuristic(int[,] grid)
        {
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            int filledCells = 0;

            for (int ai = 0; ai < 3; ai++)
            {
                for (int aj = 0; aj < 3; aj++)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (Enumerable.Range(0, 3).Any(si => Enumerable.Range(0, 3).Any(sj => grid[ai * 3 + si, aj * 3 + sj] == num)))
                        {
                            continue;
                        }

                        var possibleColumnIndexes = Enumerable.Range(0, 3).SelectMany(si =>
                            Enumerable.Range(0, 3).Select(sj => Position.Create(ai * 3 + si, aj * 3 + sj)))
                            .Where(p =>
                            {
                                if (grid[p.i, p.j] != 0)
                                {
                                    return false;
                                }
                                grid[p.i, p.j] = num;
                                bool isValidTest = CheckRow(grid, p.i) && CheckColumn(grid, p.j);
                                grid[p.i, p.j] = 0;
                                return isValidTest;
                            }).Take(2).ToArray();

                        if (possibleColumnIndexes.Length == 1)
                        {
                            grid[possibleColumnIndexes[0].i, possibleColumnIndexes[0].j] = num;
                            filledCells++;
                        }
                    }
                }
            }

            return filledCells;
        }


        public static int FillByNumberHeuristic(int[,] grid)
        {
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            int filledCells = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[i, j] != 0)
                    {
                        continue;
                    }

                    var possibleNumber = Enumerable.Range(1, 9).Where(num =>
                    {
                        grid[i, j] = num;
                        return CheckColumn(grid, j) && CheckRow(grid, i) && CheckSubArea(grid, i / 3, j / 3);
                    }).Take(2).ToArray();
                    grid[i, j] = 0;

                    if (possibleNumber.Length == 1)
                    {
                        grid[i, j] = possibleNumber[0];
                        filledCells++;
                    }
                }
            }

            return filledCells;
        }

        public static void PrintSudokuGame(int[,] grid)
        {
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
