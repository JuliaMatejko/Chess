﻿namespace Chess.Model
{
    class Move
    {
        //todo: add special moves: promotion and en passant and castling
        public string PieceName { get; set; }
        public string CurrentPosition { get; set; }
        public string NewPosition{ get; set; }

        public Move(string piecename, string currentposition, string newposition)
        {
            PieceName = piecename;
            CurrentPosition = currentposition;
            NewPosition= newposition;
        }
    }
}
