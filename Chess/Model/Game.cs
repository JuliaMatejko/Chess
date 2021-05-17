﻿using Chess.Controller;
using Chess.Model.Pieces;
using Chess.View;
using System;
using System.Collections.Generic;
using static Chess.Model.GameState;

namespace Chess.Model
{
    class Game
    {
        public static Board Board { get; } = Board.CreateABoard();
        public static Dictionary<string, Field> Fields { get; set; } = CreateFields(Board);

        public static void StartGame()
        {
            Console.WriteLine(" Let's play chess!");
            Console.WriteLine("");
            Console.WriteLine(" 1. To make a move, type a piece, current piece position and new piece position separated by a space, ex. 'pw e2 e4'");
            Console.WriteLine(" 2. To promote a pawn, type, ex. 'pw e7 e8 Q'");
            Console.WriteLine("    You can choose B|N|R|Q, where each letter corresponds to: B - bishop, N - knight, R - rook, Q - queen");
            Console.WriteLine(" 3. To castle, just move your king two squares and rook will go on the right place");
            Console.WriteLine(" 4. To resign, type 'resign'");
            Console.WriteLine(" 5. To propose a draw, type 'draw'");
            SetStartingBoard();
            BoardController.RefreshAttackedSquares();
            BoardView.PrintBoard(Board);

            while (!IsAWin && !IsADraw)
            {
                BoardController.MakeAMove();
                BoardController.RefreshAttackedSquares();
                if (PlayersAgreedToADraw)
                {
                    Console.WriteLine(" Players agreed to a draw.");
                    Console.WriteLine(" It's a draw!");
                }
                else if (PlayerResigned)
                {
                    Console.Write($" {CurrentPlayer} resigned.");
                    ChangeTurns();
                    Console.WriteLine($" {CurrentPlayer} won the game!");
                }
                else if (IsACheckmate)
                {
                    BoardView.PrintBoard(Board);
                    Console.WriteLine(" Checkmate.");
                    Console.WriteLine($" {CurrentPlayer} won the game!");
                }
                else if (IsAStalemate)
                {
                    BoardView.PrintBoard(Board);
                    Console.WriteLine(" Stalemate.");
                    Console.WriteLine(" It's a draw!");
                }
                else
                {
                    BoardView.PrintBoard(Board);
                    ResetEnPassantFlag();
                    ResetCurrentPiecesAttackingTheKing();
                    ChangeTurns();
                }
            }
        }

        public static Dictionary<string, Field> CreateFields(Board board)
        {
            Dictionary<string, Field> fields = new Dictionary<string, Field>();
            int i = 0;
            int j = 0;
            foreach (string position in Board.Positions)
            {
                if (j < Board.BoardSize)
                {
                    fields[position] = board[i][j];
                }
                else
                {
                    j = 0;
                    i++;
                    fields[position] = board[i][j];
                }
                j++;
            }
            return fields;
        }

        public static void SetStartingBoard() // set pieces on the starting position on the board
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                Fields[Board.Files[i] + "2"].Content = new Pawn(true, Board.Files[i] + "2");  // set white pawns
            }
            for (var i = 0; i < Board.BoardSize; i++)
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
            Fields["d8"].Content = new Queen(false, "d8");      // set black queen*/
            Fields["e1"].Content = new King(true, "e1");        // set white king
            WhiteKing = (King)Fields["e1"].Content;
            Fields["e8"].Content = new King(false, "e8");       // set black king
            BlackKing = (King)Fields["e8"].Content;
        }  
    }
}
