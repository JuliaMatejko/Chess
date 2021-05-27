using Chess.Model;
using Chess.Model.Pieces;
using System;
using System.Collections.Generic;
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
            if (!Program.Game.PlayerResigned && !Program.Game.PlayerOfferedADraw)
            {
                FindPieceAndValidateMove(ref chosenMove, ref move, ref piece);
                ClonePieceAndNewPositionPiece(move.NewPosition, piece, ref pieceCloned, ref newPositionContentCloned, Program.Game.Fields);
                ChangeBoard(move, piece);
                Program.Game.ResetKingCheckFlag();
                RefreshAttackedSquares();

                while (Program.Game.CurrentPlayerKingIsInCheck)
                {
                    UndoBoardChanges(move, pieceCloned, newPositionContentCloned);
                    Program.Game.ResetKingCheckFlag();
                    RefreshAttackedSquares();
                    Console.WriteLine($" It's not a valid move. Your king would be in check. Choose a differnt piece or/and field.");
                    MakeAMove();
                }
            }
            else if (Program.Game.PlayerOfferedADraw)
            {
                if (OponentAcceptsADraw())
                {
                    Program.Game.PlayersAgreedToADraw = true;
                }
                else
                {
                    Program.Game.PlayerOfferedADraw = false;
                    Console.WriteLine(" Draw denied. Make a move or resign");
                    MakeAMove();
                }
            }
        }

        private static bool OponentAcceptsADraw()
        {
            Console.Write($" {Program.Game.CurrentPlayer} offers a draw. Accept a draw? [yes|no]: ");
            string acceptADraw = Console.ReadLine();
            while (!(acceptADraw == "yes" || acceptADraw == "no"))
            {
                Console.Write($"Wrong response. Accept a draw? Type 'yes' to agree to draw or 'no' to continue the game: ");
                acceptADraw = Console.ReadLine();
            }
            return acceptADraw == "yes"; 
        }

        private static void GetInputAndValidateIt(ref string chosenMove, ref Move move)
        {
            Console.Write($" {Program.Game.CurrentPlayer} turn, make a move: ");
            chosenMove = Console.ReadLine();
            while (!UserInputIsValid(chosenMove, ref move))
            {
                Console.Write($" It's not a valid input. Type your move in a correct format: ");
                chosenMove = Console.ReadLine();
            }
        }

        private static void FindPieceAndValidateMove(ref string chosenMove, ref Move move, ref Piece piece)
        {
            piece = FindPiece(move.PieceName, move.CurrentPosition);
            while (!MoveIsPossible(piece, move))
            {
                Console.Write($" It's not a valid move. Choose a differnt piece or/and field: ");
                chosenMove = Console.ReadLine();
                move = StringToMove(chosenMove);
                piece = FindPiece(move.PieceName, move.CurrentPosition);
            }

            static bool MoveIsPossible(Piece piece, Move move)
            {
                if (piece != null && ChosenPieceIsCurrentPlayersPiece(piece))
                {
                    return MoveIsCorrectPieceMove(piece, move);
                }
                return false;


                static bool MoveIsCorrectPieceMove(Piece piece, Move move)
                {
                    return piece.NextAvailablePositions.Contains(move.NewPosition);
                }

                static bool ChosenPieceIsCurrentPlayersPiece(Piece piece)
                {
                    return (piece.IsWhite && Program.Game.CurrentPlayer == Game.Sides.White)
                            || (!piece.IsWhite && Program.Game.CurrentPlayer == Game.Sides.Black);
                }
            }
        }

        private static void ClonePieceAndNewPositionPiece(string newPosition, Piece piece, ref Piece clonedPiece, ref Piece clonedNewPositionContent, Dictionary<string, Field> fields)
        {
            clonedNewPositionContent = fields[newPosition].Content;
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

        private static void ChangeBoard(Move move, Piece piece)
        {
            if (piece.GetType() == typeof(Pawn))
            {
                Pawn pawn = (Pawn)piece;
                if (pawn.IsFirstMove && Math.Abs(Convert.ToInt32(move.NewPosition[1]) - Convert.ToInt32(move.CurrentPosition[1])) == 2)   // pawn moved two squares
                {
                    pawn.IsFirstMove = false;   // pawn cannot move two squares anymore
                    pawn.CanBeTakenByEnPassantMove = true;   // pawn might be taken en passant in a next move if other necessary conditions will occur
                    if (pawn.IsWhite)
                    {
                        Program.Game.WhitePawnThatCanBeTakenByEnPassantMove = pawn;
                    }
                    else
                    {
                        Program.Game.BlackPawnThatCanBeTakenByEnPassantMove = pawn;
                    }
                }
                else if (pawn.IsFirstMove)
                {
                    pawn.IsFirstMove = false;
                }
                else if ((pawn.IsWhite && pawn.Position[1] == '7') || (!pawn.IsWhite && pawn.Position[1] == '2'))   // promote pawn
                {
                    PawnPromotion(move, pawn.IsWhite);
                }
                else if ((pawn.IsWhite && pawn.Position[1] == '5' && move.NewPosition[0] != pawn.Position[0])
                         || (!pawn.IsWhite && pawn.Position[1] == '4' && move.NewPosition[0] != pawn.Position[0]))   // en passant capture
                {
                    Program.Game.Fields[move.NewPosition[0].ToString() + move.CurrentPosition[1].ToString()].Content = null;
                }
            }
            else if (piece.GetType() == typeof(King))
            {
                King king = (King)piece;
                if (king.IsFirstMove)
                {
                    king.IsFirstMove = false;   // king cannot castle anymore
                }
                int squaresMoved = Array.IndexOf(Board.Files, move.NewPosition[0].ToString())
                                   - Array.IndexOf(Board.Files, move.CurrentPosition[0].ToString());
                if (Math.Abs(squaresMoved) == 2)   // king castled
                {
                    string oldPosition;
                    string newPosition;
                    Rook rook;
                    if (squaresMoved == 2)   // king castled king side
                    {
                        oldPosition = "h" + move.NewPosition[1].ToString();
                        newPosition = "f" + move.NewPosition[1].ToString();
                    }
                    else   // squaresMoved == -2, king castled queen side
                    {
                        oldPosition = "a" + move.NewPosition[1].ToString();
                        newPosition = "d" + move.NewPosition[1].ToString();
                    }
                    rook = (Rook)Program.Game.Fields[oldPosition].Content;   // move a rook
                    rook.Position = newPosition;
                    Program.Game.Fields[newPosition].Content = rook;
                    Program.Game.Fields[oldPosition].Content = null;
                }
            }
            else if (piece.GetType() == typeof(Rook))
            {
                Rook rook = (Rook)piece;
                if (rook.IsFirstMove)
                {
                    rook.IsFirstMove = false;   // rook cannot castle anymore
                }
            }
            piece.Position = move.NewPosition;
            Program.Game.Fields[move.NewPosition].Content = Program.Game.Fields[move.CurrentPosition].Content;   // move piece on a new position
            Program.Game.Fields[move.CurrentPosition].Content = null;
        }

        public static void RefreshAttackedSquares()
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                for (var j = 0; j < Board.BoardSize; j++)
                {
                    Piece piece = Program.Game.Board[i][j].Content;
                    if (piece != null && piece.GetType() != typeof(King))
                    {
                        piece.ControlledSquares.Clear();
                        piece.NextAvailablePositions = piece.ReturnAvailablePieceMoves(piece.Position, Program.Game.Board);
                    }
                }
            }
            Program.Game.WhiteKing.ControlledSquares.Clear();
            Program.Game.WhiteKing.NextAvailablePositions = Program.Game.WhiteKing.ReturnAvailablePieceMoves(Program.Game.WhiteKing.Position, Program.Game.Board);
            Program.Game.BlackKing.ControlledSquares.Clear();
            Program.Game.BlackKing.NextAvailablePositions = Program.Game.BlackKing.ReturnAvailablePieceMoves(Program.Game.BlackKing.Position, Program.Game.Board);
        }

        private static void UndoBoardChanges(Move move, Piece pieceCloned, Piece newPositionContentCloned)
        {
            Program.Game.Fields[move.CurrentPosition].Content = pieceCloned;   // move piece on a previous position
            Program.Game.Fields[move.NewPosition].Content = newPositionContentCloned;   // restore a previous NewPosition content
        }

        private static void PawnPromotion(Move move, bool isWhite)
        {
            switch (move.PromotionTo)
            {
                case "Q":
                    Program.Game.Fields[move.NewPosition].Content = new Queen(isWhite, move.NewPosition);
                    break;
                case "N":
                    Program.Game.Fields[move.NewPosition].Content = new Knight(isWhite, move.NewPosition);
                    break;
                case "R":
                    Program.Game.Fields[move.NewPosition].Content = new Rook(isWhite, move.NewPosition, false);
                    break;
                case "B":
                    Program.Game.Fields[move.NewPosition].Content = new Bishop(isWhite, move.NewPosition);
                    break;
            }
            Program.Game.Fields[move.CurrentPosition].Content = null;
        }

        private static bool UserInputIsValid(string chosenMove, ref Move move)
        {
            if (chosenMove.Length == 8 && chosenMove[2] == ' ' && chosenMove[5] == ' ')
            {
                move = StringToMove(chosenMove);
                if (Piece.PieceNames.Contains(move.PieceName)
                    && Board.Positions.Contains(move.CurrentPosition)
                    && Board.Positions.Contains(move.NewPosition)
                    && !((move.PieceName == "pw" && move.CurrentPosition[1] == '7')
                          || (move.PieceName == "pb" && move.CurrentPosition[1] == '2')))
                {
                    return true;
                }
            }
            else if (chosenMove.Length == 10 && chosenMove[2] == ' ' && chosenMove[5] == ' ' && chosenMove[8] == ' ')
            {
                move = StringToMove(chosenMove);
                string[] piecesToPromote = { "Q", "N", "R", "B" };
                if (Piece.PieceNames.Contains(move.PieceName)
                    && Board.Positions.Contains(move.CurrentPosition)
                    && Board.Positions.Contains(move.NewPosition)
                    && piecesToPromote.Contains(move.PromotionTo))
                {
                    return true;
                }
            }
            else if (chosenMove == "resign")
            {
                Program.Game.PlayerResigned = true;
                return true;
            }
            else if (chosenMove == "draw")
            {
                Program.Game.PlayerOfferedADraw = true;
                return true;
            }
            return false;
        }

        private static Move StringToMove(string str)
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

        private static Piece FindPiece(string pieceName, string currentPosition)
        {
            Piece piece = Program.Game.Fields[currentPosition].Content;
            
            if (piece == null)
            {
                return null;
            }
            else if (piece.Name == pieceName)
            {
                return piece;
            }
            return null;
        }
    }     
}
