using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardGames
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            Debug();
        }

        private static void Debug()
        {

            /*
            for(int row = 0; row < board.Grid.GetLength(0); row++)
            {
                for (int col = 0; col < board.Grid.GetLength(1); col++)
                {
                    var s = board.CoordToIndex(row, col);
                    Console.WriteLine(string.Format("{0},{1}: {2,2}",row,col,s));
                }
            }*/
        }
    }
}
