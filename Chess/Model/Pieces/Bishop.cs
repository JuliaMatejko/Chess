using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Bishop : Piece
    {
        static public new string[] PieceNames => new string[] { "Bw", "Bb" };

        public Bishop(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions)
        {
            MoveRightForward(fileIndex, rankIndex, newField, board, positions);
            MoveLeftBackwards(fileIndex, rankIndex, newField, board, positions);
            MoveLeftForward(fileIndex, rankIndex, newField, board, positions);
            MoveRightBackwards(fileIndex, rankIndex, newField, board, positions);
            return positions;
        }
    }
}