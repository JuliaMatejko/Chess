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

        public override List<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            List<string> positions = new List<string>();
            positions.AddRange(StandardPawnMove(currentposition, board));
            positions.AddRange(PawnCapturesPieceMove(currentposition, board));
            foreach (var item in positions)
            {
                Console.WriteLine(item);
            }
            return positions;
           
        }

        /*
        void CorrectPawnMove(string currentposition, Board board)
        {
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

             
            StandardPawnMove(currentposition, board);
        }
    */

        List<string> StandardPawnMove(string currentposition, Board board) //zwraca listę ruchów
        {
            List<string> positions = new List<string>();

            string f = Convert.ToString(currentposition[0]);
            string r = Convert.ToString(currentposition[1]);
            int file = Array.IndexOf(Board.Files, f);
            int rank = Array.IndexOf(Board.Ranks, r);

            if (IsWhite == true)
            {
                if (IsFirstMove == true && rank < Board.boardSize - 2)
                {
                    string newposition = Board.Files[file] + Board.Ranks[rank + 2];
                    if (board[file][rank + 2].Content == null)
                    {
                        positions.Add(newposition);
                    }
                }
                if (rank < Board.boardSize)
                {
                    string newposition = Board.Files[file] + Board.Ranks[rank + 1];
                    if (board[file][rank + 1].Content == null)
                    {
                        positions.Add(newposition);
                    }
                }
                return positions;
            }
            else
            {
                if (IsFirstMove == true && rank > 2)
                {
                    string newposition = Board.Files[file] + Board.Ranks[rank - 2];
                    if (board[file][rank - 2].Content == null)
                    {
                        positions.Add(newposition);
                    }
                }
                if (rank > 1)
                {
                    string newposition = Board.Files[file] + Board.Ranks[rank - 1];
                    if (board[file][rank - 1].Content == null)
                    {
                        positions.Add(newposition);
                    }
                }
                return positions;
            }  
        }
        
        List<string> PawnCapturesPieceMove(string currentposition, Board board)
        {
            List<string> positions = new List<string>();
            int file = Array.IndexOf(Board.Files, Convert.ToString(currentposition[0]));
            int rank = Array.IndexOf(Board.Ranks, Convert.ToString(currentposition[1]));
            string newposition;
            Field field;

            if (IsWhite == true)
            {
                if (rank < Board.boardSize) // to nie bedzie potrzebne jeśli najpierw zrobię walidację ruchu TODO: walidacja, TODO: opisać ładnie działanie kodu = dokumentacja
                {
                    if (file == 0)
                    {
                        MoveOneForwardDiagonallyRight(); // to do: można jeszcze bardziej uogólnic i wykorzystać tą funkcję dla innych figur
                    }
                    else if (file == 7)
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
                if (rank > 1)
                {
                    if (file == 0)
                    {
                        MoveOneForwardDiagonallyLeft();
                    }
                    else if (file == 7)
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
            /*
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

            void MoveOneForwardDiagonallyRight() //from the 'piece position of view'
            {
                int x = IsWhite == true ? 1 : -1;
                int y = IsWhite == true ? 1 : -1;
                field = board[file + x][rank + y];
                if (field.Content != null)
                {
                    bool z = IsWhite == true ? !(field.Content.IsWhite) : field.Content.IsWhite;
                    if (z)
                    {
                        newposition = Board.Files[file + x] + Board.Ranks[rank + y];
                        positions.Add(newposition);
                    }
                }
            }

            void MoveOneForwardDiagonallyLeft() //from the 'piece position of view'
            {
                int x = IsWhite == true ? -1 : 1;
                int y = IsWhite == true ? 1 : -1;
                field = board[file + x][rank + y];
                if (field.Content != null)
                {
                    bool z = IsWhite == true ? !(field.Content.IsWhite) : field.Content.IsWhite;
                    if (z)
                    {
                        newposition = Board.Files[file + x] + Board.Ranks[rank + y];
                        positions.Add(newposition);
                    }
                }
            }
        }   
    }
        //TODO :
        // test and implement ^ v !
        //EnPassantCaptureMove()
        //PromotionMove()
}
