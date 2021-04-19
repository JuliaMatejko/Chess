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

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions) //ta funkcja  jest wogóle potzrebna? narazie teylko otrzymuje i przekazuje argumenty
        {
            MoveForward(fileIndex, rankIndex, newField, board, positions);
            MoveBack(fileIndex, rankIndex, newField, board, positions);
            MoveLeft(fileIndex, rankIndex, newField, board, positions);
            MoveRight(fileIndex, rankIndex, newField, board, positions);
            MoveRightForward(fileIndex, rankIndex, newField, board, positions);
            MoveLeftBackwards(fileIndex, rankIndex, newField, board, positions);
            MoveLeftForward(fileIndex, rankIndex, newField, board, positions);
            MoveRightBackwards(fileIndex, rankIndex, newField, board, positions);
            return positions;
        }
    }
}