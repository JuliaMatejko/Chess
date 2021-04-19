using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Queen : Piece
    {
        static public new string[] PieceNames => new string[] { "Qw", "Qb" };

        public Queen(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            MoveForward(fileIndex, rankIndex, board, positions);
            MoveBack(fileIndex, rankIndex, board, positions);
            MoveLeft(fileIndex, rankIndex, board, positions);
            MoveRight(fileIndex, rankIndex, board, positions);
            MoveRightForward(fileIndex, rankIndex, board, positions);
            MoveLeftBackwards(fileIndex, rankIndex, board, positions);
            MoveLeftForward(fileIndex, rankIndex, board, positions);
            MoveRightBackwards(fileIndex, rankIndex, board, positions);
            return positions;
        }
    }
}
