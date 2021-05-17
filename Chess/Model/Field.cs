using Chess.Model.Pieces;

namespace Chess.Model
{
    class Field
    {
        public string File { get; }
        public string Rank { get; }
        public string Name { get; }
        public Piece Content { get; set; }

        public Field(string file, string rank, Piece content)
        {
            File = file;
            Rank = rank;
            Name = File + Rank;
            Content = content;
        }
    }
}
