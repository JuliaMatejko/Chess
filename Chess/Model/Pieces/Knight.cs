using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Knight : Piece
    {
        static public new string[] PieceNames => new string[] { "kw", "kb" };

        public Knight(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions)
        {

            return positions;

        }
    }
}
