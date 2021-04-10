using Chess.Model.Pieces;

namespace Chess.Model
{
    class Field
    {
        public string File { get; set; }
        public string Rank { get; set; }
        public string Name => $"{File}{Rank}";
        public Piece Content { get; set; }

        public Field(string file, string rank, Piece content)
        {
            File = file;
            Rank = rank;
            Content = content;
        }

    }
}
