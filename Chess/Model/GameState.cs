using System;
using System.Collections.Generic;
using System.Text;

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

        public static void WinConditionMet() // todo
        {
            IsAWin = false;
        }

        public static void DrawConditionMet() // todo
        {
            IsADraw = false;
        }

        public static void ChangeTurns() // ograniczyć mozliwosc ruszania bierek przeciwnika
        {
            CurrentPlayer = CurrentPlayer == Sides.White ? CurrentPlayer = Sides.Black 
                                                         : CurrentPlayer = Sides.White;
        }
        
    }
}
