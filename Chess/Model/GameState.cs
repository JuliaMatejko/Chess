using Chess.Model.Pieces;

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
        public static bool WhiteKingIsInCheck { get; set; } = false;
        public static bool BlackKingIsInCheck { get; set; } = false;
        public static bool CurrentPlayerKingIsInCheck => CurrentPlayer == Sides.White ? WhiteKingIsInCheck : BlackKingIsInCheck;
        public static bool IsAWin => WinConditionMet();
        public static bool IsADraw => DrawConditionMet();
        // flags
        public static bool PlayerResigned { get; set; } = false; // to do: implementation may change in the future so that the resignation is not only possible for the current player 
        public static bool PlayerOfferedADraw { get; set; } = false; // to do: implementation may change in the future so that offering a draw is not only possible for the current player
        public static bool PlayersAgreedToADraw { get; set; } = false;
        public static bool IsAStalemate => StalemateOccured();
        public static bool IsACheckmate { get; set; } = false;


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

        /* methods checking if it is a win */
        
        public static bool WinConditionMet() // todo
        {
            return IsACheckmate || PlayerResigned;
        }

        /*static bool IsACheckmate()
        {
            return false; // todo: implement the logic
        }*/

        /* methods checking if it is a draw */
        public static bool DrawConditionMet() // todo
        {
           /*
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

        static bool StalemateOccured() // not optimal, create list of pieces for both players? to do
        {
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    Piece piece = Game.board[i][j].Content;
                    
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
    }
}
