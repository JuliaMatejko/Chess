using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    abstract class Piece
    {
        public bool IsWhite { get; set; }
        public string Name { get; set; }    // piece representation on the board/ in the future: przekazywane do kontrolera i wyswietlane odpowiednio w warstw. prezentacji?
        public string Position { get; set; } //field.name board[][].Name
        public List<string> NextAvailablePositions
        {
            set => nextAvailablePositions = value;
            get => ReturnAvailablePieceMoves(Position, Game.board);    // fields available for piece to move in ongoing round
        }
        private List<string> nextAvailablePositions;
        public static string[] PieceNames => new string[] { "pw", "pb", "Rw", "Rb", "kw", "kb", "Bw", "Bb", "Qw", "Qb", "Kw", "Kb"};

        protected List<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            int file = Array.IndexOf(Board.Files, Convert.ToString(currentposition[0]));
            int rank = Array.IndexOf(Board.Ranks, Convert.ToString(currentposition[1]));
            Field field = null;
            List<string> positions = new List<string>();
            positions.AddRange(ReturnCorrectPieceMoves(file, rank, field, board, positions));
            /* foreach (var item in positions)
             {
                 Console.WriteLine(item);
             }*/
            return positions;

        }

        protected abstract List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field field, Board board, List<string> positions);

    }
}
