using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Rook : Piece
    {
        public Rook(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int file, int rank, Field field, Board board, List<string> positions)
        {

            return positions;

        }
    }
}