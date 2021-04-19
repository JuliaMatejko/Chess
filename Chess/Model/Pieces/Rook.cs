using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Rook : Piece
    {
        public static new string[] PieceNames => new string[] { "Rw", "Rb" };
        public bool IsFirstMove { get; set; } = true;

        public Rook(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions)
        {
            MoveForward(fileIndex, rankIndex, newField, board, positions);
            MoveBack(fileIndex, rankIndex, newField, board, positions);
            MoveLeft(fileIndex, rankIndex, newField, board, positions);
            MoveRight(fileIndex, rankIndex, newField, board, positions);
            return positions;
        }
    }
}