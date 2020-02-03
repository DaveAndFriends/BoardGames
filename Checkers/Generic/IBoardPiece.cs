using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoardGames.Generic
{
    public interface IBoardPiece
    {
        bool Team { get; set; }
    }
}
