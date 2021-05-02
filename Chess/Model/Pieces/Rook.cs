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

        public Rook(bool iswhite, string position, bool isfirstmove)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
            IsFirstMove = isfirstmove;
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            MoveForward(fileIndex, rankIndex, board, positions);
            MoveBackwards(fileIndex, rankIndex, board, positions);
            MoveLeft(fileIndex, rankIndex, board, positions);
            MoveRight(fileIndex, rankIndex, board, positions);
            return positions;
        }
    }
}
