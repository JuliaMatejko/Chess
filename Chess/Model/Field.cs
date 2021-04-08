using Chess.Model.Pieces;

namespace Chess.Model
{
    class Field
    {
        public string Column { get; set; }
        public string Row { get; set; }
        public string Name => $"{Column}{Row}";
        public Piece Content { get; set; }

        public Field(string column, string row, Piece content)
        {
            Column = column;
            Row = row;
            Content = content;
        }

    }
}
