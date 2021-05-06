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
        public static bool IsADraw { get; set; } = false;
        public static bool PlayerResigned { get; set; } = false;
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

        /*public static void PlayerResigned() // TO DO
        {
            IsAWin = true; // todo: implement the logic
        }*/

        /* methods checking if it is a draw */
        public static void DrawConditionMet() // todo
        {
            if (IsAStalemate()) // automatic draw, important, must have
            {
                IsADraw = true;
            }
            else if (IsADeadPosition()) // automatic draw
            {
                IsADraw = true;
            }
            else if (PlayersAgreedToADraw()) // automatic draw, important, must have    TO DO
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

            IsADraw = false;
        }

        static bool IsAStalemate()
        {
            return false; // todo: implement the logic
        }

        static bool IsADeadPosition()
        {
            return false; // todo: implement the logic
        }

        static bool PlayersAgreedToADraw()
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
