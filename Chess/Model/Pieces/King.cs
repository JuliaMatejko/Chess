using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class King : Piece
    {
        static public new string[] PieceNames => new string[] { "Kw", "Kb" };

        public King(bool iswhite, string position)
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

        public override List<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            List<string> positions = new List<string>();
            //positions.AddRange(StandardPawnMove(currentposition, board));
            //positions.AddRange(PawnCapturesPieceMove(currentposition, board));
            foreach (var item in positions)
            {
                Console.WriteLine(item);
            }
            return positions;

        }
    }
}