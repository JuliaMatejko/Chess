using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    abstract class Piece : ICloneable
    {
        public bool IsWhite { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public HashSet<string> NextAvailablePositions { get; set; } = new HashSet<string>();
        public HashSet<string> ControlledSquares { get; set; } = new HashSet<string>();
        public static string[] PieceNames { get; } = new string[] { "pw", "pb", "Rw", "Rb", "Nw", "Nb", "Bw", "Bb", "Qw", "Qb", "Kw", "Kb" };

        public HashSet<string> ReturnAvailablePieceMoves(string currentPosition, Board board)
        {
            int fileIndex = Array.IndexOf(Board.Files, Convert.ToString(currentPosition[0]));
            int rankIndex = Array.IndexOf(Board.Ranks, Convert.ToString(currentPosition[1]));
            HashSet<string> positions = new HashSet<string>();
            positions = ReturnCorrectPieceMoves(fileIndex, rankIndex, board, positions);
            return positions;
        }

        protected abstract HashSet<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, HashSet<string> positions);

        public object Clone()
        {
            return this != null ? MemberwiseClone() : null;
        }
    }
}

