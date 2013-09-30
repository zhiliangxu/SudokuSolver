using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SudokuSolver
{
    [TestClass]
    public class HeuristicTest
    {
        [TestMethod]
        public void Test_RowBasedPositionHeuristic_OneCellFilled()
        {
            int[,] grid = {
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 1, 0, 7, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 2, 0},
                               {0, 0, 2, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0}
                           };
            int filledCells = SudokuSolver.FillByRowBasedPositionHeuristic(grid);
            Assert.AreEqual(1, filledCells);
            Assert.AreEqual(2, grid[3, 4]);
        }

        [TestMethod]
        public void Test_ColumnBasedPositionHeuristic_OneCellFilled()
        {
            int[,] grid = {
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 7, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 1, 0, 0},
                               {0, 0, 0, 0, 0, 0, 2, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 7}
                           };
            int filledCells = SudokuSolver.FillByColumnBasedPositionHeuristic(grid);
            Assert.AreEqual(1, filledCells);
            Assert.AreEqual(7, grid[5, 6]);
        }

        [TestMethod]
        public void Test_SubAreaBasedPositionHeuristic_OneCellFilled()
        {
            int[,] grid = {
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 6, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 2, 5, 0, 0, 0, 0},
                               {0, 0, 0, 1, 0, 0, 0, 0, 0},
                               {0, 6, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0}
                           };
            int filledCells = SudokuSolver.FillBySubAreaBasedPositionHeuristic(grid);
            Assert.AreEqual(1, filledCells);
            Assert.AreEqual(6, grid[4, 4]);
        }

        [TestMethod]
        public void Test_Heuristic_ZeroCellFilled()
        {
            int[,] grid = new int[9, 9];

            int filledCells = SudokuSolver.FillByColumnBasedPositionHeuristic(grid);
            Assert.AreEqual(0, filledCells);

            filledCells = SudokuSolver.FillByRowBasedPositionHeuristic(grid);
            Assert.AreEqual(0, filledCells);

            filledCells = SudokuSolver.FillBySubAreaBasedPositionHeuristic(grid);
            Assert.AreEqual(0, filledCells);

            filledCells = SudokuSolver.FillByNumberHeuristic(grid);
            Assert.AreEqual(0, filledCells);
        }

        [TestMethod]
        public void Test_NumberHeuristic_OneCellFilled()
        {
            int[,] grid = {
                               {0, 0, 0, 0, 5, 0, 0, 0, 0},
                               {0, 0, 0, 0, 6, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {1, 2, 0, 0, 0, 0, 0, 3, 4},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 7, 0, 0, 0, 0},
                               {0, 0, 0, 0, 8, 0, 0, 0, 0}
                           };
            int filledCells = SudokuSolver.FillByNumberHeuristic(grid);
            Assert.AreEqual(1, filledCells);
            Assert.AreEqual(9, grid[4, 4]);
        }

        [TestMethod]
        public void Test_Heuristic_SolveAll()
        {
            int[,] grid = {
                               {0, 0, 1, 0, 4, 0, 5, 0, 7},
                               {4, 6, 0, 3, 0, 7, 2, 0, 0},
                               {0, 0, 9, 0, 0, 0, 0, 4, 3},
                               {0, 1, 0, 0, 0, 3, 9, 5, 0},
                               {9, 0, 2, 7, 0, 5, 0, 3, 0},
                               {0, 5, 0, 9, 0, 0, 7, 0, 6},
                               {6, 0, 0, 0, 0, 0, 1, 0, 0},
                               {1, 0, 5, 0, 0, 4, 0, 2, 8},
                               {8, 4, 0, 2, 0, 0, 6, 9, 0}
                           };
            int pendingCells = grid.Cast<int>().Where(num => num == 0).Count();

            int filledCells = SudokuSolver.SolveHeuristically(grid);
            Assert.AreEqual(pendingCells, filledCells);
            Assert.IsTrue(grid.Cast<int>().All(num => num != 0));
            Assert.IsTrue(SudokuSolver.IsValidSudokuGame(grid));
        }

        [TestMethod]
        public void Test_Solve_Hard()
        {
            int[,] grid = {
                               {0, 0, 0, 0, 0, 0, 0, 6, 8},
                               {0, 9, 5, 0, 0, 6, 7, 0, 2},
                               {0, 0, 0, 0, 0, 7, 0, 0, 0},
                               {0, 0, 0, 0, 4, 5, 3, 0, 0},
                               {0, 5, 6, 0, 3, 0, 4, 1, 0},
                               {0, 0, 3, 8, 6, 0, 0, 0, 0},
                               {0, 0, 0, 5, 0, 0, 0, 0, 0},
                               {4, 0, 9, 3, 0, 0, 8, 5, 0},
                               {5, 2, 0, 0, 0, 0, 0, 0, 0}
                           };
            bool solved = SudokuSolver.Solve(grid);
            Assert.IsTrue(solved);
            Assert.IsTrue(grid.Cast<int>().All(num => num != 0));
            Assert.IsTrue(SudokuSolver.IsValidSudokuGame(grid));
        }

        [TestMethod]
        public void Test_Solve_NoSulution()
        {
            int[,] grid = {
                               {1, 0, 0, 0, 0, 0, 0, 6, 8},
                               {0, 9, 5, 0, 0, 6, 7, 0, 2},
                               {0, 0, 0, 0, 0, 7, 0, 0, 0},
                               {0, 0, 0, 0, 4, 5, 3, 0, 0},
                               {0, 5, 6, 0, 3, 0, 4, 1, 0},
                               {0, 0, 3, 8, 6, 0, 0, 0, 0},
                               {0, 0, 0, 5, 0, 0, 0, 0, 0},
                               {4, 0, 9, 3, 0, 0, 8, 5, 0},
                               {5, 2, 0, 0, 0, 0, 0, 0, 0}
                           };
            bool solved = SudokuSolver.Solve(grid);
            Assert.IsFalse(solved);
        }

        [TestMethod]
        public void Test_Solve_Sparse()
        {
            int[,] grid = {
                               {0, 0, 0, 0, 0, 3, 0, 0, 0},
                               {0, 0, 8, 0, 0, 0, 0, 0, 1},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {2, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 7, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 9, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0}
                           };
            bool solved = SudokuSolver.Solve(grid);
            Assert.IsTrue(solved);
            Assert.IsTrue(grid.Cast<int>().All(num => num != 0));
            Assert.IsTrue(SudokuSolver.IsValidSudokuGame(grid));
        }

        private void AreIdentical(int[,] grid1, int[,] grid2)
        {
            Assert.AreEqual(grid1.GetLength(0), grid2.GetLength(0));
            Assert.AreEqual(grid1.GetLength(1), grid2.GetLength(1));

            for (int i = 0; i < grid1.GetLength(0); i++)
            {
                for (int j = 0; j < grid1.GetLength(1); j++)
                {
                    Assert.AreEqual(grid1[i, j], grid2[i, j]);
                }
            }
        }
    }
}
