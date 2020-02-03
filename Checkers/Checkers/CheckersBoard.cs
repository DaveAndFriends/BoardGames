using BoardGames.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoardGames
{
    public class CheckersBoard
    {
        private const int size = 8;
        public CheckersTile[,] Grid { get; set; }

        public CheckersBoard()
        {
            Grid = new CheckersTile[size, size];
            bool startingType;

            //Create an empty board
            for (int row = 0; row < size; row++)
            {
                if (row % 2 == 0)
                    startingType = true;
                else
                    startingType = false;

                for (int col = 0; col < size; col++)
                {
                    if (col % 2 == 0)
                        Grid[row, col] = new CheckersTile(startingType, null);
                    else
                        Grid[row, col] = new CheckersTile(!startingType, null);
                }
            }

            //Fill board with initial pieces
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    CheckersTile tile = Grid[row, col];
                    if (!tile.Type)
                    {
                        if (row < 3)
                            tile.Piece = new CheckersPiece(false);
                        else if (row >= 5)
                            tile.Piece = new CheckersPiece(true);
                    }
                }
            }
        }

        public int CoordToIndex(Tuple<int,int> coords)
        {
            int x = coords.Item1;
            int y = coords.Item2;

            //if grid square is black and the coords are within bounds
            if (x >= 0 && x < size && y >= 0 && y < size && x!=y)
                    return Grid[x,y].GetIndex();
            return -1;
        }

        public Tuple<int,int> IndexToCoord(int index)
        {
            if (index >= 0 && index <= size * size && index <= (size * size) / 2)
            {
                int count = 1;

                for (int row = 0; row < size; row++)
                {
                    bool isIndexable;
                    if (row % 2 == 0)
                        isIndexable = false;
                    else
                        isIndexable = true;

                    for (int col = 0; col < size; col++)
                    {
                        if (isIndexable)
                        {
                            if (count >= index)
                                return new Tuple<int, int>(row, col);
                            count++;
                        }
                        isIndexable = !isIndexable;
                    }
                }
            }
            return null;
        }

        public List<Tuple<int,int>> GetValidMoves()
        {
            var result = new List<Tuple<int, int>>();
            var maxIndex = size * size;

            for(int i = 1; i <= maxIndex; i++)
                for(int j = 1; j <= maxIndex; j++)
                    if (GetPiece(i) != null && GetPiece(j) == null && GetMoveType(i, j) != MoveType.Invalid)
                        result.Add(new Tuple<int, int>(i, j));

            return result;
        }

        public bool MovePiece(int startIdx, int endIdx)
        {
            var startTile = GetTile(startIdx);
            var endTile = GetTile(endIdx);
            Direction dir = GetMoveDirection(startIdx, endIdx);
            CheckersTile intTile;

            switch (GetMoveType(startIdx, endIdx))
            {
                case MoveType.Simple:
                    if (IsKingMove(startIdx, endIdx))
                        startTile.Piece.IsKing = true;
                    endTile.Piece = startTile.Piece;
                    startTile.Piece = null;
                    return true;

                case MoveType.Jump:
                    intTile = GetAdjacentTile(startTile, dir);
                    intTile.Piece = null;
                    if (IsKingMove(startIdx, endIdx))
                        startTile.Piece.IsKing = true;
                    endTile.Piece = startTile.Piece;
                    startTile.Piece = null;
                    return true;

                case MoveType.MultiJump:
                    var jumpStartTile = GetTile(startIdx);
                    while (jumpStartTile.GetIndex() != endIdx)
                    {
                        intTile = GetAdjacentTile(jumpStartTile, dir);
                        intTile.Piece = null;
                        jumpStartTile = GetAdjacentTile(intTile, dir);
                    }
                    if (IsKingMove(startIdx, endIdx))
                        startTile.Piece.IsKing = true;
                    endTile.Piece = startTile.Piece;
                    startTile.Piece = null;
                    return true;

                default:
                    return false;
            }
        }

        private MoveType GetMoveType(int startIdx, int endIdx)
        {
            if (IsValidSimpleMove(startIdx, endIdx))
                return MoveType.Simple;
            else if (IsValidJump(startIdx, endIdx))
                return MoveType.Jump;
            else if (IsValidMultiJump(startIdx, endIdx))
                return MoveType.MultiJump;
            else
                return MoveType.Invalid;
        }

        private bool IsValidSimpleMove(int startIndex, int endIndex)
        {
            var start = IndexToCoord(startIndex);
            var end = IndexToCoord(endIndex);
            if (start != null && end != null)
            {
                var startTile = Grid[start.Item1, start.Item2];
                var adjTiles = GetAdjacentTiles(startTile);
                var endTile = Grid[end.Item1, end.Item2];
                var startIdx = CoordToIndex(start);
                var endIdx = CoordToIndex(end);
                var idxDiff = startIdx - endIdx;

                if (startTile.Piece != null && endTile.Piece == null && adjTiles.Contains(endTile))
                {
                    var piece = startTile.Piece;

                    if (piece.IsKing)
                        return true;

                    if ((piece.Team && (idxDiff > 0)) || (!piece.Team && (idxDiff < 0)))
                        return true;
                }
            }
            return false;
        }

        private bool IsValidJump(int startIndex, int endIndex)
        {
            var start = IndexToCoord(startIndex);
            var end = IndexToCoord(endIndex);

            if (start != null && end != null && Math.Abs(start.Item1 - end.Item1) == 2)
            {

                Direction dir = GetMoveDirection(startIndex, endIndex);
                var startTile = Grid[start.Item1, start.Item2];
                var endTile = Grid[end.Item1, end.Item2];
                var startIdx = CoordToIndex(start);
                var endIdx = CoordToIndex(end);
                var idxDiff = startIdx - endIdx;

                if (startTile.Piece != null &&      //if the start tile is occupied
                    endTile.Piece == null &&        //if the destination tile is unoccupied
                    (Math.Abs(idxDiff) == 7 ||        //if the destination is in the valid range
                    Math.Abs(idxDiff) == 9))
                {

                    var startPiece = startTile.Piece;
                    var capturedPiece = GetAdjacentTile(startTile, dir).Piece;

                    if (capturedPiece != null && startPiece.Team != capturedPiece.Team)
                    {
                        if (startPiece.IsKing)
                        {
                            return true;
                        }
                        else if (startPiece.Team && (idxDiff == 7 || idxDiff == 9) || (!startPiece.Team && (idxDiff == -7 || idxDiff == -9)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsValidMultiJump(int startIdx, int endIdx)
        {
            var startTile = GetTile(startIdx);
            var endTile = GetTile(endIdx);

            if (startTile != null && endTile != null && startTile!=endTile)
            {
                Direction dir = GetMoveDirection(startIdx, endIdx);
                var jumpStartTile = startTile;
                var startPiece = GetPiece(startTile.GetIndex());

                while (jumpStartTile != GetTile(endIdx))
                {
                    var jumpIntTile = GetAdjacentTile(jumpStartTile, dir);
                    if (jumpIntTile != null)
                    {
                        var jumpEndTile = GetAdjacentTile(jumpIntTile, dir);
                        var jumpStartPiece = jumpStartTile.Piece;
                        jumpStartTile.Piece = startPiece;
                        if (jumpEndTile == null || !IsValidJump(jumpStartTile.GetIndex(), jumpEndTile.GetIndex()))
                        {
                            jumpStartTile.Piece = jumpStartPiece;
                            return false;
                        }
                        jumpStartTile.Piece = jumpStartPiece;
                        jumpStartTile = jumpEndTile;
                    }
                    else
                        return false;
                }
                return true;
            }
            return false;
        }

        private bool IsKingMove(int startIdx, int endIdx)
        {
            var piece = GetTile(startIdx).Piece;
            if (piece.IsKing || (piece.Team && endIdx <= 4) || (!piece.Team && endIdx >= 29))
                return true;
            return false;
        }

        private CheckersTile GetAdjacentTile(CheckersTile tile, Direction dir)
        {
            var startCoords = IndexToCoord(tile.GetIndex());

            if (startCoords != null)
            {
                switch (dir)
                {
                    case Direction.UpLeft:
                        return GetTile(CoordToIndex(new Tuple<int, int>(startCoords.Item1 - 1, startCoords.Item2 - 1)));
                    case Direction.UpRight:
                        return GetTile(CoordToIndex(new Tuple<int, int>(startCoords.Item1 - 1, startCoords.Item2 + 1)));
                    case Direction.DownLeft:
                        return GetTile(CoordToIndex(new Tuple<int, int>(startCoords.Item1 + 1, startCoords.Item2 - 1)));
                    case Direction.DownRight:
                        return GetTile(CoordToIndex(new Tuple<int, int>(startCoords.Item1 + 1, startCoords.Item2 + 1)));
                    default:
                        return null;
                }
            }

            return null;
        }

        private List<CheckersTile> GetAdjacentTiles(CheckersTile tile)
        {
            var result = new List<CheckersTile>();

            foreach(Direction dir in Enum.GetValues(typeof(Direction)))
            {
                result.Add(GetAdjacentTile(tile, dir));
            }

            return result;
        }

        private CheckersTile GetTile(int index)
        {
            var coords = IndexToCoord(index);
            if(coords != null)
                return Grid[coords.Item1, coords.Item2];
            return null;
        }

        private CheckersPiece GetPiece(int index)
        {
            var tile = GetTile(index);
            if (tile != null)
                return tile.Piece;
            return null;
        }

        private Direction GetMoveDirection(int startIdx, int endIdx)
        {
            var startCoords = IndexToCoord(startIdx);
            var endCoords = IndexToCoord(endIdx);
            var xDiff = startCoords.Item1 - endCoords.Item1;
            var yDiff = startCoords.Item2 - endCoords.Item2;

            if (xDiff > 0) //up
            { 
                if (yDiff > 0) //left
                    return Direction.UpLeft;
                return Direction.UpRight;
            }
            else //down
            {
                if (yDiff > 0) //;eft
                    return Direction.DownLeft;
                return Direction.DownRight;
            }
        }

        private enum Direction
        {
            UpRight,
            UpLeft,
            DownRight,
            DownLeft,
        }

        private enum MoveType
        {
            Simple,
            Jump,
            MultiJump,
            Invalid
        }

        /*
        private class CheckersMove
        {
            public int StartIdx { get; }
            public int EndIdx { get; }
            public bool Team { get; }
            public CheckersPiece Piece { get; }
            public MoveType MoveType { get; }
            public Direction Direction { get; }


            public CheckersMove(int startIdx, int endIdx, CheckersBoard board)
            {
                var startTile = board.GetTile(startIdx);
                StartIdx = startIdx;
                EndIdx = endIdx;
                Piece = startTile.Piece;
                Team = Piece.Team;
                MoveType = board.GetMoveType(startIdx, endIdx);
                Direction = board.GetMoveDirection(startIdx, endIdx);
            }
        }
        */
    }
}
