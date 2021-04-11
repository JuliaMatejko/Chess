using Chess.Controller;
using Chess.Model.Pieces;
using Chess.View;
using System;
using System.Collections.Generic;

namespace Chess.Model
{
    class Game
    {
        public static Board board = new Board();
        public static Dictionary<string, Field> Fields => SetFieldDictionary(board);
        static public string[] Positions => CreatePositionNames(); //przenieść?do board

        public static void StartGame()
        {
            Console.WriteLine(" Let's play chess!");
            Console.WriteLine("");
            Console.WriteLine(" To make a move, type a piece, current piece position and new piece position separated by a space, ex. 'pw e2 e4'");
            Board.CreateABoard(board);
            SetStartingBoard(board);
            BoardView.PrintBoard(board);


            while (!GameState.IsAWin && !GameState.IsADraw)
            {
                BoardController.MakeAMove();
                BoardView.PrintBoard(board);
                GameState.WinConditionMet();
                GameState.DrawConditionMet();

                if (!GameState.IsAWin && !GameState.IsADraw)
                {
                    GameState.ChangeTurns();
                }
                else
                {
                    if (GameState.IsAWin)
                    {
                        Console.WriteLine($" {GameState.CurrentPlayer} won the game!");
                    }
                    else if (GameState.IsADraw)
                    {
                        Console.WriteLine(" It's a draw!");
                    }
                }
            }
        }

        public static void SetStartingBoard(Board board) // set pieces at starting position on the board
        {
            for (int i = 0; i < Board.boardSize; i++)
            {
                Fields[Board.Files[i] + "2"].Content = new Pawn(true, Board.Files[i] + "2");  // set white pawns
            }
            for (int i = 0; i < Board.boardSize; i++)
            {
                Fields[Board.Files[i] + "7"].Content = new Pawn(false, Board.Files[i] + "7");  // set black pawns 
            }
            Fields["a1"].Content = new Rook(true, "a1");        // set white rooks
            Fields["h1"].Content = new Rook(true, "h1");
            Fields["a8"].Content = new Rook(false, "a8");       // set black rooks
            Fields["h8"].Content = new Rook(false, "h8");
            Fields["b1"].Content = new Knight(true, "b1");      // set white knights
            Fields["g1"].Content = new Knight(true, "g1");
            Fields["b8"].Content = new Knight(false, "b8");     // set black knights
            Fields["g8"].Content = new Knight(false, "g8");
            Fields["c1"].Content = new Bishop(true, "c1");      // set white bishops
            Fields["f1"].Content = new Bishop(true, "f1");
            Fields["c8"].Content = new Bishop(false, "c8");     // set black bishops
            Fields["f8"].Content = new Bishop(false, "f8");
            Fields["d1"].Content = new Queen(true, "d1");       // set white queen
            Fields["d8"].Content = new Queen(false, "d8");      // set black queen
            Fields["e1"].Content = new King(true, "e1");        // set white king
            Fields["e8"].Content = new King(false, "e8");       // set black king
        }

        static Dictionary<string, Field> SetFieldDictionary(Board board)
        {
            Dictionary<string, Field> dictionary = new Dictionary<string, Field>();

            int count = 0;
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    dictionary.Add(Positions[count], board[i][j]);
                    count++;
                }
            }
            return dictionary;
        }

        static string[] CreatePositionNames()
        {
            string[] fieldnames = new string[Board.boardSize * Board.boardSize];

            int count = 0;
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    fieldnames[count] = Board.Files[i] + Board.Ranks[j];
                    count++;
                }
            }
            return fieldnames;
        }
    }
}
