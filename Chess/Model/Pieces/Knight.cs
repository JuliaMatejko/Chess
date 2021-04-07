namespace Chess.Model.Pieces
{
    class Knight : Piece
    {
        static public new string[] PieceNames => new string[] { "kw", "kb" };

        public Knight(bool iswhite, string position)
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