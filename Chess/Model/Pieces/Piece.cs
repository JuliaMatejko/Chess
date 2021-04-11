using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    abstract class Piece
    {
        public bool IsWhite { get; set; }   // true for first player -> white side player, false for second player -> black side player
        public string Name { get; set; }    // piece representation on the board/ in the future: przekazywane do kontrolera i wyswietlane odpowiednio w warstw. prezentacji?
        public string Position { get; set; } //field.name
        public List<string> NextAvailablePositions
        {
            set => nextAvailablePositions = value;
            get
            {
                return ReturnAvailablePieceMoves(Position, Game.board);    // fields available for piece to move in ongoing round
            }
        }
        private List<string> nextAvailablePositions;

        static public string[] PieceNames => new string[] { "pw", "pb", "Rw", "Rb", "kw", "kb", "Bw", "Bb", "Qw", "Qb", "Kw", "Kb"};

        abstract public List<string> ReturnAvailablePieceMoves(string currentposition, Board board); // to do: przeksztalcic na abstract? uogolnic korzystajac z implementacji z pawn

    }
}
