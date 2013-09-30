using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SudokuSolver
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void CompletedSudokuGame_Valid()
        {
            int[,] grid = {
                               {1, 2, 3, 4, 5, 6, 7, 8, 9},
                               {4, 5, 6, 7, 8, 9, 1, 2, 3},
                               {7, 8, 9, 1, 2, 3, 4, 5, 6},
                               {2, 3, 4, 5, 6, 7, 8, 9, 1},
                               {5, 6, 7, 8, 9, 1, 2, 3, 4},
                               {8, 9, 1, 2, 3, 4, 5, 6, 7},
                               {3, 4, 5, 6, 7, 8, 9, 1, 2},
                               {6, 7, 8, 9, 1, 2, 3, 4, 5},
                               {9, 1, 2, 3, 4, 5, 6, 7, 8}
                           };
            Assert.IsTrue(SudokuSolver.IsValidSudokuGame(grid));
        }

        [TestMethod]
        public void CompletedSudokuGame_Invalid()
        {
            int[,] grid = {
                               {1, 2, 3, 4, 5, 6, 7, 8, 9},
                               {4, 5, 4, 7, 8, 9, 1, 2, 3},
                               {7, 8, 9, 1, 2, 3, 4, 5, 6},
                               {2, 3, 6, 5, 6, 7, 8, 9, 1},
                               {5, 6, 7, 8, 9, 1, 2, 3, 4},
                               {8, 9, 1, 2, 3, 4, 5, 6, 7},
                               {3, 4, 5, 6, 7, 8, 9, 1, 2},
                               {6, 7, 8, 9, 1, 2, 3, 4, 5},
                               {9, 1, 2, 3, 4, 5, 6, 7, 8}
                           };
            Assert.IsFalse(SudokuSolver.IsValidSudokuGame(grid));
        }

        [TestMethod]
        public void IncompleteSudokuGame_Valid()
        {
            int[,] grid = {
                               {1, 2, 0, 4, 5, 6, 7, 8, 9},
                               {4, 5, 6, 7, 8, 9, 1, 2, 3},
                               {7, 8, 9, 1, 2, 3, 4, 5, 6},
                               {2, 3, 4, 5, 6, 7, 8, 9, 1},
                               {5, 6, 7, 8, 9, 1, 2, 3, 4},
                               {8, 9, 1, 2, 3, 4, 5, 6, 7},
                               {3, 4, 5, 6, 7, 8, 0, 1, 2},
                               {6, 7, 8, 9, 1, 2, 3, 4, 5},
                               {9, 1, 2, 3, 4, 5, 6, 7, 8}
                           };
            Assert.IsTrue(SudokuSolver.IsValidSudokuGame(grid));
        }

        [TestMethod]
        public void IncompleteSudokuGame_Invalid()
        {
            int[,] grid = {
                               {1, 2, 3, 4, 5, 6, 7, 8, 9},
                               {4, 5, 6, 7, 8, 9, 1, 2, 3},
                               {7, 8, 9, 1, 2, 3, 4, 5, 6},
                               {2, 3, 4, 5, 6, 7, 8, 9, 1},
                               {5, 6, 7, 8, 9, 1, 2, 3, 4},
                               {8, 9, 1, 2, 3, 4, 5, 6, 0},
                               {3, 4, 5, 6, 0, 8, 9, 1, 7},
                               {6, 7, 8, 9, 1, 2, 3, 4, 5},
                               {9, 1, 2, 3, 4, 5, 6, 7, 8}
                           };
            Assert.IsFalse(SudokuSolver.IsValidSudokuGame(grid));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSudokuGame_Throws_ArgumentNullException()
        {
            int[,] grid = null;
            SudokuSolver.IsValidSudokuGame(grid);
        }

        [TestMethod]
        public void InvalidGridSize_Invalid()
        {
            int[,] grid = new int[0, 0];
            Assert.IsFalse(SudokuSolver.IsValidSudokuGame(grid));

            grid = new int[9, 10];
            Assert.IsFalse(SudokuSolver.IsValidSudokuGame(grid));

            grid = new int[10, 9];
            Assert.IsFalse(SudokuSolver.IsValidSudokuGame(grid));
        }
    }
}
