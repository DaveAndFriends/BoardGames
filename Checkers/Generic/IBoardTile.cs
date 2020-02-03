using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoardGames.Generic
{
    public interface IBoardTile
    {
        bool Type { get; set; }
        IBoardPiece Piece { get; set; }
    }
}
