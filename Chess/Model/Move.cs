namespace Chess.Model
{
    class Move
    {
        public string PieceName { get; set; }
        public string CurrentPosition { get; set; }
        public string NewPosition { get; set; }
        public string PromotionTo { get; set; }

        public Move(string pieceName, string currentPosition, string newPosition)
        {
            PieceName = pieceName;
            CurrentPosition = currentPosition;
            NewPosition= newPosition;
        }

        public Move(string pieceName, string currentPosition, string newPosition, string promotionTo)
        {
            PieceName = pieceName;
            CurrentPosition = currentPosition;
            NewPosition = newPosition;
            PromotionTo = promotionTo;
        }
    }
}
