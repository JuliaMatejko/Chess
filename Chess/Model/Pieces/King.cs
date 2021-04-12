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

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field field, Board board, List<string> positions)
        {
            //type of moves: vertical, horizontal, diagonal, not in line of attack,special: castle
            VerticalKingMove(positions, fileIndex, rankIndex, field, board);
            HorizontalKingMove(positions, fileIndex, rankIndex, field, board);
            DiagonalKingMove(positions, fileIndex, rankIndex, field, board);
            //Move validation! not in line of attack();
            //CastleKingRookMove(); czy powinien byc w tej klasie?

            return positions;

        }

        List<string> DiagonalKingMove(List<string> positions, int fileIndex, int rankIndex, Field field, Board board)
        {
            throw new NotImplementedException();
        }

        List<string> HorizontalKingMove(List<string> positions, int fileIndex, int rankIndex, Field field, Board board)
        {
            throw new NotImplementedException();
        }

        List<string> VerticalKingMove(List<string> positions, int fileIndex, int rankIndex, Field field, Board board)
        {
            //IsWhite == true   nie wiem czy musze dla czarnego pisac inny kod, na razie pisze dla bialego
            if (rankIndex > 0 && rankIndex < Board.boardSize - 1)
            {
                field = board[fileIndex][rankIndex + 1];
            }
            return positions;

            void MoveOneForward()
            {
                int y = IsWhite == true ? 1 : -1;
                field = board[fileIndex][rankIndex + y];
                if (field.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
            }
        }
    }
}