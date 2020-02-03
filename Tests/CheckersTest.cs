using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BoardGames
{
    [TestClass]
    public class CheckersTest
    {
        [TestMethod]
        public void TestCoordsAndIndexes()
        {
            CheckersBoard board = new CheckersBoard();
            Tuple<int, int> coords = null;

            for(int i = 0; i < board.Grid.Length; i++)
            {
                coords = board.IndexToCoord(i);
            }
        }
    }
}
