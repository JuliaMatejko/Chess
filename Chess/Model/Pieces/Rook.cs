namespace Chess.Model.Pieces
{
    class Rook : Piece
    {
        static public new string[] PieceNames => new string[] { "Rw", "Rb" };

        public Rook(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            if (iswhite == true)
            {
                Name = PieceNames[0];
            }
            else
            {
                Name = PieceNames[1];
            }
        }
    }
}