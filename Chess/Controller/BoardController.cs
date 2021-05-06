using Chess.Model;
using Chess.Model.Pieces;
using System;
using System.Linq;

namespace Chess.Controller
{
    class BoardController
    {
        public static void MakeAMove()
        {
            string chosenMove = null;
            Move move = null;
            Piece piece = null;
            Piece pieceCloned = null;
            Piece newPositionContentCloned = null;

            GetInputAndValidateIt(ref chosenMove, ref move);
            if (!GameState.PlayerResigned)
            {
                FindPieceAndValidateMove(ref chosenMove, ref move, ref piece);
                ClonePieceAndNewPositionPiece(move.NewPosition, piece, ref pieceCloned, ref newPositionContentCloned);
                ChangeBoard(move, piece);
                GameState.ResetKingCheckFlag();
                RefreshAttackedSquares();

                while (GameState.CurrentPlayerKingIsInCheck)
                {
                    UndoBoardChanges(move, pieceCloned, newPositionContentCloned);
                    GameState.ResetKingCheckFlag();
                    RefreshAttackedSquares();
                    Console.WriteLine($" It's not a valid move. Your king would be in check. Choose a differnt piece or/and field.");
                    MakeAMove();
                }
            }
        }

        static void GetInputAndValidateIt(ref string chosenMove, ref Move move)
        {
            Console.Write($" {GameState.CurrentPlayer} turn, make a move: ");
            chosenMove = Console.ReadLine();
            while (!UserInputIsValid(chosenMove, ref move))
            {
                Console.Write($" It's not a valid input. Type your move in a correct format: ");
                chosenMove = Console.ReadLine();
            }
        }

        static void FindPieceAndValidateMove(ref string chosenMove, ref Move move, ref Piece piece)
        {
            piece = FindPiece(move.PieceName, move.CurrentPosition);
            while (!MoveValidator.MoveIsPossible(piece, move))
            {
                Console.Write($" It's not a valid move. Choose a differnt piece or/and field: ");
                chosenMove = Console.ReadLine();
                move = StringToMove(chosenMove);
                piece = FindPiece(move.PieceName, move.CurrentPosition);
            }
        }

        static void ClonePieceAndNewPositionPiece(string newposition, Piece piece, ref Piece clonedPiece, ref Piece clonedNewPositionContent)
        {
            clonedNewPositionContent = Game.Fields[newposition].Content;
            if (clonedNewPositionContent != null)
            {
                clonedNewPositionContent = (Piece)clonedNewPositionContent.Clone();
            }
            else
            {
                clonedNewPositionContent = null;
            }
            clonedPiece = (Piece)piece.Clone();
        }

        static void ChangeBoard(Move move, Piece piece)
        {
            if (piece.GetType() == typeof(Pawn))
            {
                Pawn pawn = (Pawn)piece;
                if (pawn.IsFirstMove && Math.Abs(Convert.ToInt32(move.NewPosition[1]) - Convert.ToInt32(move.CurrentPosition[1])) == 2) // move two squares
                {
                    pawn.IsFirstMove = false; // pawn cannot move two squares anymore
                    pawn.CanBeTakenByEnPassantMove = true; // pawn might be taken en passant in a next move if other necessary conditions will occur
                    if (pawn.IsWhite)
                    {
                        GameState.WhitePawnThatCanBeTakenByEnPassantMove = pawn;
                    }
                    else
                    {
                        GameState.BlackPawnThatCanBeTakenByEnPassantMove = pawn;
                    }
                }
                else if (pawn.IsFirstMove)
                {
                    pawn.IsFirstMove = false;
                }
                else if ((pawn.IsWhite && pawn.Position[1] == '7') || (!pawn.IsWhite && pawn.Position[1] == '2')) // promote pawn
                {
                    PawnPromotion(move, pawn.IsWhite);
                }
                else if ((pawn.IsWhite && pawn.Position[1] == '5' && move.NewPosition[0] != pawn.Position[0])
                         || (!pawn.IsWhite && pawn.Position[1] == '4' && move.NewPosition[0] != pawn.Position[0])) // en passant capture
                {
                    Game.Fields[move.NewPosition.Substring(0, 1) + move.CurrentPosition.Substring(1, 1)].Content = null;
                }
            }
            else if (piece.GetType() == typeof(King))
            {
                King king = (King)piece;
                if (king.IsFirstMove)
                {
                    king.IsFirstMove = false; // king cannot castle anymore
                }
                int squaresMoved = Array.IndexOf(Board.Files, move.NewPosition.Substring(0, 1))
                                   - Array.IndexOf(Board.Files, move.CurrentPosition.Substring(0, 1));
                if (Math.Abs(squaresMoved) == 2) // king castled
                {
                    string oldPosition;
                    string newPosition;
                    Rook rook;
                    if (squaresMoved == 2) // castled king side
                    {
                        oldPosition = "h" + move.NewPosition.Substring(1, 1);
                        newPosition = "f" + move.NewPosition.Substring(1, 1);
                    }
                    else // squaresMoved == -2, castled queen side
                    {
                        oldPosition = "a" + move.NewPosition.Substring(1, 1);
                        newPosition = "d" + move.NewPosition.Substring(1, 1);
                    }
                    rook = (Rook)Game.Fields[oldPosition].Content; // move a rook
                    rook.Position = newPosition;
                    Game.Fields[newPosition].Content = rook;
                    Game.Fields[oldPosition].Content = null;
                }
            }
            else if (piece.GetType() == typeof(Rook))
            {
                Rook rook = (Rook)piece;
                if (rook.IsFirstMove)
                {
                    rook.IsFirstMove = false; // rook cannot castle anymore
                }
            }
            piece.Position = move.NewPosition;
            Game.Fields[move.NewPosition].Content = Game.Fields[move.CurrentPosition].Content; // move piece on a new position
            Game.Fields[move.CurrentPosition].Content = null;
        }

        public static void RefreshAttackedSquares()// to do przenies
        {
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    Piece piece = Game.board[i][j].Content;
                    if (piece != null)
                    {
                        piece.NextAvailablePositions = piece.ReturnAvailablePieceMoves(piece.Position, Game.board);
                    }
                }
            }
        }

        static void UndoBoardChanges(Move move, Piece pieceCloned, Piece newPositionContentCloned)
        {
            Game.Fields[move.CurrentPosition].Content = pieceCloned; // move piece on a previous position
            Game.Fields[move.NewPosition].Content = newPositionContentCloned; // restore a previous NewPosition content
        }

        static void PawnPromotion(Move move, bool iswhite)
        {
            switch (move.PromotionTo)
            {
                case "Q":
                    Game.Fields[move.NewPosition].Content = new Queen(iswhite, move.NewPosition);
                    break;
                case "N":
                    Game.Fields[move.NewPosition].Content = new Knight(iswhite, move.NewPosition);
                    break;
                case "R":
                    Game.Fields[move.NewPosition].Content = new Rook(iswhite, move.NewPosition, false);
                    break;
                case "B":
                    Game.Fields[move.NewPosition].Content = new Bishop(iswhite, move.NewPosition);
                    break;
            }
            Game.Fields[move.CurrentPosition].Content = null;
        }

        static bool UserInputIsValid(string chosenmove, ref Move move)
        {
            if (chosenmove.Length == 8 && chosenmove[2] == ' ' && chosenmove[5] == ' ')
            {
                move = StringToMove(chosenmove);
                if (Piece.PieceNames.Contains(move.PieceName)
                    && Board.Positions.Contains(move.CurrentPosition)
                    && Board.Positions.Contains(move.NewPosition)
                    && !((move.PieceName == "pw" && move.CurrentPosition[1] == '7')
                          || (move.PieceName == "pb" && move.CurrentPosition[1] == '2')))
                {
                    return true;
                }
            }
            else if (chosenmove.Length == 10 && chosenmove[2] == ' ' && chosenmove[5] == ' ' && chosenmove[8] == ' ')
            {
                move = StringToMove(chosenmove);
                string[] piecesToPromote = { "Q", "N", "R", "B" };
                if (Piece.PieceNames.Contains(move.PieceName)
                    && Board.Positions.Contains(move.CurrentPosition)
                    && Board.Positions.Contains(move.NewPosition)
                    && piecesToPromote.Contains(move.PromotionTo))
                {
                    return true;
                }
            }
            else if (chosenmove == "resign")
            {
                GameState.PlayerResigned = true;
                return true;
            }
            /*else if (chosenmove == "draw")
            {

            }*/
            return false;
        }

        static Move StringToMove(string str)
        {
            string[] substrings = str.Split(" ");
            if (str.Length == 8)
            {
                return new Move(substrings[0], substrings[1], substrings[2]);
            }
            else
            {
                return new Move(substrings[0], substrings[1], substrings[2], substrings[3]);
            }
        }

        static Piece FindPiece(string piecename, string currentposition)
        {
            Piece piece = Game.Fields[currentposition].Content;
            
            if (piece == null)
            {
                return null;
            }
            else if (piece.Name == piecename)
            {
                return piece;
            }
            return null;
        }
    }     
}
