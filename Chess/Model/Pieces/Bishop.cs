﻿using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Bishop : Piece
    {
        static public new string[] PieceNames => new string[] { "Bw", "Bb" };

        public Bishop(bool iswhite, string position)
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

        protected override List<string> ReturnCorrectPieceMoves(int file, int rank, Field field, Board board, List<string> positions)
        {
            
            return positions;
            
        }

    }
}