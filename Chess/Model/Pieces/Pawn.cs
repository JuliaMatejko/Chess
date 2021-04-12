using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Pawn : Piece
    {
        public static new string[] PieceNames => new string[] { "pw", "pb" };
        public bool IsFirstMove { get; set; } = true;

        public Pawn(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite == true ? Name = PieceNames[0] : Name = PieceNames[1];
            NextAvailablePositions = ReturnAvailablePieceMoves(Position, Game.board);
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field field, Board board, List<string> positions)
        {
            ForwardPawnMove(positions, fileIndex, rankIndex, field, board);
            DiagonalForwardPawnMove(positions, fileIndex, rankIndex, field, board);
            //EnPassantPawnMove(); TODO
            //PromotionPawnMove(); TODO
            return positions;
            /*
             * Pawns have the most complex rules of movement:
              1.  A pawn moves straight forward one square, if that square is vacant.    a2 -> a3
              2. If it has not yet moved, a pawn also has the option of moving two squares straight forward, provided both squares are vacant.   a2 -> a4
              3. Pawns cannot move backwards. a2 -/-> a1

             4. A pawn, unlike other pieces, captures differently from how it moves. A pawn can capture an enemy piece on either of the two squares diagonally in front of the pawn 
                (but cannot move to those squares if they are vacant).
             5. The pawn is also involved in the two special moves en passant and promotion
                    - promotion:
                        If a player advances a pawn to its eighth rank, the pawn is then promoted (converted) to a queen, rook, bishop, or knight of the same color
                        at the choice of the player (a queen is usually chosen). The choice is not limited to previously captured pieces.
                    - en passant:
                        When a pawn advances two squares from its original square and ends the turn adjacent to a pawn of the opponent's on the same rank, it may be captured by
                        that pawn of the opponent's, as if it had moved only one square forward. This capture is only legal on the opponent's next move immediately following 
                        the first pawn's advance.


            Ad 1. Ad 2. obecna pozycja +1 if its first move, obecna pozycja +1 lub +2 (granicą jest boardSize)

             
            */
        }

        List<string> ForwardPawnMove(List<string> positions, int fileIndex, int rankIndex, Field field, Board board)
        {   
            if (IsWhite == true)
            {
                if (IsFirstMove == true)
                {
                    MoveTwoForward();
                }
                if (rankIndex < Board.boardSize - 1)
                {
                    MoveOneForward();
                }
                return positions;
            }
            else
            {
                if (IsFirstMove == true)
                {
                    MoveTwoForward();
                }
                if (rankIndex > 0)
                {
                    MoveOneForward();
                }
                return positions;
            }

            void MoveOneForward()
            {
                int y = IsWhite == true ? 1 : -1;
                field = board[fileIndex][rankIndex + y];
                if (field.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
            }

            void MoveTwoForward()
            {
                int y = IsWhite == true ? 2 : -2;
                field = board[fileIndex][rankIndex + y];
                if (field.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
            }
        }
        
        List<string> DiagonalForwardPawnMove(List<string> positions, int fileIndex, int rankIndex, Field field, Board board)
        {
            if (IsWhite == true)
            {
                if (rankIndex < Board.boardSize - 1)
                {
                    if (fileIndex == 0)
                    {
                        MoveOneForwardDiagonallyRight(); // to do: można jeszcze bardziej uogólnic i wykorzystać tą funkcję dla innych figur?
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneForwardDiagonallyLeft();
                    }
                    else
                    {
                        MoveOneForwardDiagonallyRight();
                        MoveOneForwardDiagonallyLeft();
                    }
                }
                return positions;
            }
            else
            {
                if (rankIndex > 0)
                {
                    if (fileIndex == 0)
                    {
                        MoveOneForwardDiagonallyLeft();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneForwardDiagonallyRight();
                    }
                    else
                    {
                        MoveOneForwardDiagonallyLeft();
                        MoveOneForwardDiagonallyRight();
                    }
                }
                return positions;
            }

            void MoveOneForwardDiagonallyRight() // from the 'piece position of view' - black moves left on the board view
            {
                int x = IsWhite == true ? 1 : -1;
                int y = IsWhite == true ? 1 : -1;
                field = board[fileIndex + x][rankIndex + y];
                if (field.Content != null && field.Content.GetType() != typeof(King))
                {
                    bool z = IsWhite == true ? !(field.Content.IsWhite) : field.Content.IsWhite;
                    if (z)
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                }
            }

            void MoveOneForwardDiagonallyLeft() // from the 'piece position of view' - black moves right on the board view
            {
                int x = IsWhite == true ? -1 : 1;
                int y = IsWhite == true ? 1 : -1;
                field = board[fileIndex + x][rankIndex + y];
                if (field.Content != null && field.Content.GetType() != typeof(King))
                {
                    bool z = IsWhite == true ? !(field.Content.IsWhite) : field.Content.IsWhite;
                    if (z)
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                }
            }
        }   
    }
    //TODO :
    // test and implement ^ v !
    //EnPassantCaptureMove()
    //PromotionMove()

    /* ARCHIVE
void WhiteMoveOneForwardDiagonallyRight()
{
    field = board[file + 1][rank + 1];
    if (field.Content != null && !(field.Content.IsWhite))
    {
        newposition = Board.Files[file + 1] + Board.Ranks[rank + 1];
        positions.Add(newposition);
    }
}

void WhiteMoveOneForwardDiagonallyLeft()
{
    field = board[file - 1][rank + 1];
    if (field.Content != null && !(field.Content.IsWhite))
    {
        newposition = Board.Files[file - 1] + Board.Ranks[rank + 1];
        positions.Add(newposition);
    }
}

void BlackMoveOneForwardDiagonallyRight()
{
    field = board[file + 1][rank - 1];
    if (field.Content != null && field.Content.IsWhite)
    {
        newposition = Board.Files[file + 1] + Board.Ranks[rank - 1];
        positions.Add(newposition);
    }
}

void BlackMoveOneForwardDiagonallyLeft() //można jeszcze uprościć i połączyć funkcje
{
    field = board[file - 1][rank - 1];
    if (field.Content != null && field.Content.IsWhite)
    {
        newposition = Board.Files[file - 1] + Board.Ranks[rank - 1];
        positions.Add(newposition);
    }
}*/

}
