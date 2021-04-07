namespace Chess.Model.Pieces
{
    class Pawn : Piece
    {
        static public new string[] PieceNames => new string[] { "pw", "pb" };

        public Pawn(bool iswhite, string position)
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