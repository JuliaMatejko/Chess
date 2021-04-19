namespace Chess.Model
{
    class GameState
    {
        public enum Sides
        {
            White = 1,
            Black = 0
        }
        static public Sides CurrentPlayer { get; set; } =  Sides.White;
        static public bool IsAWin { get; set; } = false;
        static public bool IsADraw { get; set; } = false;
        static public bool[,] WhiteControlledSquares => new bool[,] { { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false }};// => SetControlledSquares(Game.board);
        static public bool[,] BlackControlledSquares => new bool[,] { { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false },
                                                                      { false, false, false, false, false, false, false, false }};

        static bool[,] SetControlledSquares()
        {
            /*
             Sprawdzam każdą figurę, jej dostępne ruchy, jeśli zawiera dane pole to zmieniam jego wartość na true, jesli nie to false i tak z każdą figurą, pomijam pola z wartością null
            Robię to osobno dla białych i czarnych.


            var arr = board[i][j].Content.NextAvailablePositions
            foreach( string position in arr)
            {
                
            }
             */
            bool[,] controlledSquares = new bool[8,8];
            
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    if (Game.board[i][j].Content != null)
                    {
                        

                        if (Game.board[i][j].Content.NextAvailablePositions.)
                        {
                            controlledSquares[i, j] = true;
                        }
                        else
                        {
                            controlledSquares[i, j] = false;
                        }
                    }
                }
            }
            return controlledSquares;
        }
        public static void ChangeTurns() // ograniczyć mozliwosc ruszania bierek przeciwnika
        {
            CurrentPlayer = CurrentPlayer == Sides.White ? CurrentPlayer = Sides.Black
                                                         : CurrentPlayer = Sides.White;
        }

        /* methodes checking if it is a win */
        public static void WinConditionMet() // todo kiedy? Gdy checkmate (lub skończył ięczas, ale na razie bez czasu). Kiedy? 
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

        static bool PlayerResigned()
        {
            return false; // todo: implement the logic
        }

        /* methodes checking if it is a draw */
        public static void DrawConditionMet() // todo kiedy jest remis? - pat, -niewystarczający materiał, -50 ruchów bez bicia czy jakoś tak, -powtórzenie pozycji
        {
            if (IsAStalemate()) // automatic draw, important, must have. rest, if I have time
            {
                IsADraw = true;
            }
            else if (IsADeadPosition()) // automatic draw
            {
                IsADraw = true;
            }
            else if (PlayersAgreedToADraw()) // automatic draw, important, must have
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
