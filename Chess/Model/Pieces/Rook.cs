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
            if (iswhite == true)
            {
                Name = PieceNames[2];
            }
            else
            {
                Name = PieceNames[3];
            }
        }

        protected override List<string> ReturnCorrectPieceMoves(int file, int rank, Field field, Board board, List<string> positions)
        {

            return positions;

        }
    }
}