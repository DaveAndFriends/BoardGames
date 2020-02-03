using BoardGames.Generic;
using System;
using System.Drawing;

namespace BoardGames
{
    public class CheckersTile
    {
        private static int id = 0;

        public int ID { get; }
        public bool Type { get; set; }
        public CheckersPiece Piece { get; set; }

        public CheckersTile(bool type, CheckersPiece piece)
        {
            Piece = piece;
            Type = type;
            ID = id;
            id++;
        }

        public int GetIndex()
        {
            if (!Type)
                return (int)Math.Ceiling((ID + 1) / 2.0);
            return -1;
        }
    }
}
