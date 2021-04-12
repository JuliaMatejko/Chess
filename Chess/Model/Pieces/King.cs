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

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions)
        {
            //type of moves: vertical, horizontal, diagonal, not in line of attack,special: castle
            VerticalKingMove(positions, fileIndex, rankIndex, newField, board);
            //HorizontalKingMove(positions, fileIndex, rankIndex, field, board);
            //DiagonalKingMove(positions, fileIndex, rankIndex, field, board);
            //Move validation! not in line of attack();
            //CastleKingRookMove(); czy powinien byc w tej klasie?

            return positions;

        }

        List<string> DiagonalKingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            throw new NotImplementedException();
        }

        List<string> HorizontalKingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            throw new NotImplementedException();
        }

        List<string> VerticalKingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            if (rankIndex < Board.boardSize - 1)
            {
                if (IsWhite == true)
                {
                    MoveOneForward();
                }
                else
                {
                    MoveOneBackwards();
                }
            }
            if (rankIndex > 0)
            {
                if (IsWhite == true)
                {
                    MoveOneBackwards();
                }
                else
                {
                    MoveOneForward();
                }
            }
            return positions;
            
            void MoveOneForward()       // TODO : królnie powinien móc się ruszyć na pole które jest atakowane przez figurę przeciwnika 
            {                           // ( jeśli NextAvailable moves którejś z figur przeciwnika zawiera pole na które chce się ruszyć król)
                int y = IsWhite == true ? 1 : -1;
                newField = board[fileIndex][rankIndex + y];
                if (PlayerControlledFields.Contains(newField.Name))
                {

                }
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
                if (newField.Content != null && newField.Content.GetType() != typeof(King))
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
            }

            void MoveOneBackwards()
            {
                int y = IsWhite == true ? -1 : 1;
                newField = board[fileIndex][rankIndex + y];
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
                if (newField.Content != null && newField.Content.GetType() != typeof(King))
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
            }
        }
    }
}