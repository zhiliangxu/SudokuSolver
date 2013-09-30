
namespace SudokuSolver
{
    internal struct Position
    {
        public int i;
        public int j;

        internal static Position Create(int i, int j)
        {
            return new Position { i = i, j = j };
        }
    }
}
