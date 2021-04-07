using Chess.Model.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Model
{
    class Board : List<List<Field>> 
    {
        /* board representation => two dimmentional array
          
                     {
                       {a1, a2, ..., a7, a8},
                       {b1, b2, ..., b7, b8},
                       {c1, c2, ..., c7, c8},
       white side      {d1, d2, ..., d7, d8},   black side
                       {e1, e2, ..., e7, e8},
                       {f1, f2, ..., f7, f8},
                       {g1, g2, ..., g7, g8},
                       {h1, h2, ..., h7, h8}
                     }
         */

        public const int boardSize = 8;
        static public string[] Columns => new string[boardSize] { "a", "b", "c", "d", "e", "f", "g", "h" };
        static public string[] Rows => new string[boardSize] { "1", "2", "3", "4", "5", "6", "7", "8" };
        

        static public void CreateBoard(Board board)
        {
            for (int i = 0; i < boardSize; i++)
            {
                board.Add(new List<Field>());   // creates column A, B, C,..., H

                for (int j = 0; j < boardSize; j++)
                {
                    board[i].Add(new Field(Columns[i], Rows[j], null));   // creates fields in the column, ex. a1, a2,..., a8
                }
            } 
        }

 
        

    }
}
