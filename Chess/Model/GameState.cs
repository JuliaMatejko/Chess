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
        static public Sides CurrentPlayer { get; set; } = Sides.White;
        static public Pawn WhitePawnThatCanBeTakenByEnPassantMove { get; set; }
        static public Pawn BlackPawnThatCanBeTakenByEnPassantMove { get; set; }
        static public bool WhiteKingIsInCheck { get; set; } = false;
        static public bool BlackKingIsInCheck { get; set; } = false;
        static public bool CurrentPlayerKingIsInCheck => CurrentPlayer == Sides.White ? WhiteKingIsInCheck : BlackKingIsInCheck;
        static public bool IsAWin { get; set; } = false;
        static public bool IsADraw { get; set; } = false;

        public static void ChangeTurns()
        {
            ResetEnPassantFlag();
            CurrentPlayer = CurrentPlayer == Sides.White ? CurrentPlayer = Sides.Black
                                                         : CurrentPlayer = Sides.White;
            static void ResetEnPassantFlag()
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
        public static void WinConditionMet() // todo
        {
            if (IsACheckmate())
            {
                IsAWin = true;
            }
            if (PlayerResigned())
            {
                IsAWin = true;
            }
            IsAWin = false;
        }

        static bool IsACheckmate()
        {
            return false; // todo: implement the logic
        }

        static bool PlayerResigned() // TO DO
        {
            return false; // todo: implement the logic
        }

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
