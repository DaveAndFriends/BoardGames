using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.Generic;

namespace BoardGames
{
    public class CheckersPiece
    {
        private static int id = 0;
        public int ID { get; }

        public bool IsKing { get; set; }
        public bool Team { get; set; }

        public CheckersPiece(bool team)
        {
            IsKing = false;
            Team = team;
            ID = id;
            id++;
        }
    }
}
