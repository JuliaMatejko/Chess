using Chess.Model.Pieces;
using System;
using System.Collections.Generic;

namespace Chess.Model
{
    class GameState
    {
        public enum Sides
        {
            White = 1,
            Black = 0
        }
        public static Sides CurrentPlayer { get; set; } = Sides.White;
        public static Pawn WhitePawnThatCanBeTakenByEnPassantMove { get; set; }
        public static Pawn BlackPawnThatCanBeTakenByEnPassantMove { get; set; }
        public static King WhiteKing { get; set; }
        public static King BlackKing { get; set; }
        public static bool WhiteKingIsInCheck { get; set; } = false;
        public static bool BlackKingIsInCheck { get; set; } = false;
        public static bool CurrentPlayerKingIsInCheck => CurrentPlayer == Sides.White ? WhiteKingIsInCheck : BlackKingIsInCheck;
        public static bool IsAWin => WinConditionMet();
        public static bool IsADraw => DrawConditionMet();
        public static bool PlayerResigned { get; set; } = false;        // TO DO someday: implementation may change in the future so that the resignation is not only possible for the current player
        public static bool PlayerOfferedADraw { get; set; } = false;    // TO DO someday: implementation may change in the future so that offering a draw is not only possible for the current player
        public static bool PlayersAgreedToADraw { get; set; } = false;
        public static bool IsAStalemate => StalemateOccured();
        public static List<Piece> CurrentPlayerPiecesAttackingTheKing { get; set; } = new List<Piece>();
        public static bool IsACheckmate => CheckmateOccured();


        public static void ChangeTurns()
        {
            CurrentPlayer = CurrentPlayer == Sides.White ? CurrentPlayer = Sides.Black
                                                         : CurrentPlayer = Sides.White;
        }

        public static void ResetEnPassantFlag()
        {
            if (CurrentPlayer == Sides.White)
            {
                if (BlackPawnThatCanBeTakenByEnPassantMove != null)
                {
                    BlackPawnThatCanBeTakenByEnPassantMove.CanBeTakenByEnPassantMove = false;
                    BlackPawnThatCanBeTakenByEnPassantMove = null;
                }
            }
            else
            {
                if (WhitePawnThatCanBeTakenByEnPassantMove != null)
                {
                    WhitePawnThatCanBeTakenByEnPassantMove.CanBeTakenByEnPassantMove = false;
                    WhitePawnThatCanBeTakenByEnPassantMove = null;
                }
            }
        }

        public static void ResetCurrentPiecesAttackingTheKing()
        {
            CurrentPlayerPiecesAttackingTheKing.Clear();
        }

        public static void ResetKingCheckFlag()
        {
            if (CurrentPlayer == Sides.White)
            {
                WhiteKingIsInCheck = false;
            }
            else
            {
                BlackKingIsInCheck = false;
            }
        }
        
        public static bool WinConditionMet()
        {
            return IsACheckmate || PlayerResigned;
        }

        private static bool CheckmateOccured()
        {
            bool oponentsKingIsInCheck = CurrentPlayer == Sides.White ? BlackKingIsInCheck : WhiteKingIsInCheck;
            if (oponentsKingIsInCheck)
            {
                bool oponentsKingIsDoubleChecked = CurrentPlayerPiecesAttackingTheKing.Count > 1;
                if (oponentsKingIsDoubleChecked && !KingCanMoveAway())
                {
                    return true;
                }
                else if (!KingCanMoveAway())
                {
                    if (CurrentPlayerPiecesAttackingTheKing[0].GetType() == typeof(IDiagonallyMovingPiece)
                        || CurrentPlayerPiecesAttackingTheKing[0].GetType() == typeof(IHorizontallyAndVerticallyMovingPiece))
                    {
                        if (!CheckCanBeBlockedOrAttackingPieceCanBeCaptured())
                        {
                            return true;
                        }
                    }
                    else 
                    {
                        if (!AttackingPieceCanBeCaptured())
                        {
                            return true;
                        } 
                    }
                }
            }
            return false;

            bool KingCanMoveAway()
            {
                int oponentsKingAvailablePositionsCount = CurrentPlayer == Sides.White ? BlackKing.NextAvailablePositions.Count 
                                                                                       : WhiteKing.NextAvailablePositions.Count;
                return oponentsKingAvailablePositionsCount != 0;
            }

            bool AttackingPieceCanBeCaptured()
            {
                for (var i = 0; i < Board.BoardSize; i++)
                {
                    for (var j = 0; j < Board.BoardSize; j++)
                    {
                        Piece piece = Game.Board[i][j].Content;
                        if (piece != null)
                        {
                            bool isOponentsPiece = CurrentPlayer == Sides.White ? !piece.IsWhite : piece.IsWhite;
                            if (isOponentsPiece)
                            {
                                if (piece.NextAvailablePositions.Contains(CurrentPlayerPiecesAttackingTheKing[0].Position))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            bool CheckCanBeBlockedOrAttackingPieceCanBeCaptured() // Return positions that are blocking check
            {
                List<string> blockingPositions = new List<string>();                                                                      
                int currentPlayersPieceFile = Array.IndexOf(Board.Files, CurrentPlayerPiecesAttackingTheKing[0].Position.Substring(0, 1));
                int currentPlayersPieceRank = Array.IndexOf(Board.Ranks, CurrentPlayerPiecesAttackingTheKing[0].Position.Substring(1, 1));
                King oponentsKing = CurrentPlayer == Sides.White ? BlackKing : WhiteKing;
                int oponentsKingFile = Array.IndexOf(Board.Files, oponentsKing.Position.Substring(0, 1));
                int oponentsKingRank = Array.IndexOf(Board.Ranks, oponentsKing.Position.Substring(1, 1));

                int x = oponentsKingFile == currentPlayersPieceFile ? 0 : (oponentsKingFile > currentPlayersPieceFile ? 1 : -1);    // Check if both pieces are on the same file --> A rook or a queen is an attacking piece
                int y = oponentsKingRank == currentPlayersPieceRank ? 0 : (oponentsKingRank > currentPlayersPieceRank ? 1 : -1);    // Check if both pieces are on the same rank --> A rook or a queen is an attacking piece
                int file = currentPlayersPieceFile + x;                                                                             // Otherwise, pieces are on the different file and rank-- > A bishop or a queen is an attacking piece
                int rank = currentPlayersPieceRank + y;

                bool canMoveInBetween = y == 0 ? (oponentsKingFile > currentPlayersPieceFile ? file < oponentsKingFile : file > oponentsKingFile) 
                                                : (oponentsKingRank > currentPlayersPieceRank ? rank < oponentsKingRank : rank > oponentsKingRank);
                while (canMoveInBetween)
                {
                    blockingPositions.Add(Board.Files[file] + Board.Ranks[rank]);
                    file += x;
                    rank += y;
                }
                    
                for (int i = 0; i < Board.BoardSize; i++)
                {
                    for (int j = 0; j < Board.BoardSize; j++)
                    {
                        Piece piece = Game.Board[i][j].Content;
                        if (piece != null)
                        {
                            bool isOponentsPiece = CurrentPlayer == Sides.White ? !piece.IsWhite : piece.IsWhite;
                            if (isOponentsPiece)
                            {
                                if (piece.NextAvailablePositions.Contains(CurrentPlayerPiecesAttackingTheKing[0].Position))    // Check if any of the oponents pieces can capture attacking piece
                                {
                                    return true;
                                }
                                foreach (string position in blockingPositions)                                                 // Check if any of the oponents pieces can move on the blocking position
                                {
                                    if (piece.NextAvailablePositions.Contains(position))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

        public static bool DrawConditionMet()
        {
           /*   TODO someday:
            if (IsADeadPosition()) // automatic draw
            {
                IsADraw = true;
            }
            else if (IsAFivefoldRepetition()) // automatic draw
            {
                IsADraw = true;
            }
            else if (IsASeventyFiveMoveRule()) // automatic draw
            {
                IsADraw = true;
            }
            else if (IsAFiftyMoveRule()) // not automatic draw
            {
                IsADraw = true;
            }
            else if (IsAThreefoldRepetition()) // not automatic draw
            {
                IsADraw = true;
            }
            */
            return PlayersAgreedToADraw || IsAStalemate;
        }

        static bool StalemateOccured()
        {
            for (int i = 0; i < Board.BoardSize; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Piece piece = Game.Board[i][j].Content;
                    if (piece != null) 
                    {
                        bool isOponentsPiece = CurrentPlayer == Sides.White ? !piece.IsWhite : piece.IsWhite;
                        if (isOponentsPiece && piece.NextAvailablePositions.Count != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /*    TODO someday:
        static bool IsADeadPosition()
        {
            return false; // todo: implement the logic
        }
        
        static bool IsAFivefoldRepetition()
        {
            return false; // todo: implement the logic
        }

        static bool IsASeventyFiveMoveRule()
        {
            return false; // todo: implement the logic
        }

        static bool IsAFiftyMoveRule()
        {
            return false; // todo: implement the logic
        }

        static bool IsAThreefoldRepetition()
        {
            return false; // todo: implement the logic
        } 
        */
    }
}
