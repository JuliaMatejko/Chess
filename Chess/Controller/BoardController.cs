using Chess.Model;
using Chess.Model.Pieces;
using Chess.View;
using System;
using System.Collections.Generic;

namespace Chess.Controller
{
    class BoardController // nie do konca rozumiem czym jest controller, doczytac, poprzenosić members jesli trzeba
    {
        static Board board = new Board();
        static public string[] Positions => CreatePositionNames();
        public static Dictionary<string, Field> Fields => SetFieldDictionary(board);

        


        static void Main(string[] args)
        {
            Console.WriteLine("Let's play chess!");
            Console.WriteLine("");
            // program start

            Board.CreateBoard(board);
            SetStartingBoard(board);
            BoardView.PrintBoard(board);
            Console.WriteLine(" To make a move, type a piece, current piece position and new piece position separated by a space, ex. 'pw e2 e4'");

            while (!(GameState.IsAWin || GameState.IsADraw))
            {
                MakeAMove();
                BoardView.PrintBoard(board);
                WinConditionMet();
                DrawConditionMet();
            }
            if (!(GameState.IsAWin || GameState.IsADraw))
            {
                ChangeTurns();
            }
            else
            {
                if (GameState.IsAWin)
                {
                    Console.WriteLine($" '{GameState.CurrentPlayer}' won the game!");
                }
                else if (GameState.IsADraw)
                {
                    Console.WriteLine(" It's a draw!");
                }
            }
            /*
            Board.CreateBoard(board);
            SetStartingBoard(board);
            BoardView.PrintBoard();
            Console.WriteLine("");
            Console.WriteLine(" To make a move, type a piece, current piece position and new piece position separated by a space, ex. 'pw e2 e4'");
            MakeAMove();
            BoardView.PrintBoard();*/
            // program end
        }
        
        //to do start
        static void MakeAMove()
        {
            Console.Write(" Make a move: ");
            string chosenMove = Console.ReadLine();

            Move move = StringToMove(chosenMove);
            while (!MoveIsPossible(move))
            {
                Console.WriteLine($" It's not a valid move. Choose a differnt piece or/and field: "); //to do --> more specific?
                chosenMove = Console.ReadLine();
            }
            Fields[move.NewPosition].Content = Fields[move.CurrentPosition].Content;
            Fields[move.CurrentPosition].Content = null;
        }

        static bool MoveIsPossible(Move move) // ograniczyć rucszanie nie swoimi bierkami
        {
            if (true)//MoveIsValid(move) && MoveIsLegal(move))
            {
                return true;
            }
            return false;
        }

        static void WinConditionMet() // todo oraz przeniesc?
        {
            GameState.IsAWin = false;
        }

        static void DrawConditionMet() // todo oraz przeniesc?
        {
            GameState.IsADraw = false;
        }

        static void ChangeTurns() // ograniczyć mozliwosc ruszania bierek przeciwnika
        {
            GameState.CurrentPlayer
                = GameState.CurrentPlayer == GameState.Sides.White ? GameState.CurrentPlayer = GameState.Sides.Black
                                                                   : GameState.CurrentPlayer = GameState.Sides.White;
        }
        /*
        bool MoveIsLegal(Move move)
        {
            if (MoveIsPrinciplePieceMove(move) && !ChosenPieceProtectsKingCheck(move))
            {
                return true;
            }
            return false;
        }

        bool MoveIsPrinciplePieceMove(Move move) //! .... TO DO
        {
            if (NextAvailablePositions.Contains(move.field))
            {
                return true;
            }
            return false;
            //return true;
        }

        void ReturnAvailablePieceMoves(Move move)
        {
            List<string> availablePieceMoves = new List<string>();
            //pawn
            //MoveOneForward();
            //MoveTwoForward();


        }

        bool ChosenPieceProtectsKingCheck(Move move) //! .... TO DO
        {
                if (true)
                {
                    return true;
                }
                return false;
                return false;
            }

        bool MoveIsValid(Move move)
        {
            if (ChosenPieceIsValid(Convert.ToString(move.PieceName)) && ChosenFieldIsValid(Convert.ToString(move.NewField)))
            {
                return true;
            }
            return false;
        }

        bool ChosenFieldIsValid(string field) // chosen field is on the board
        {
            if (Positions.Contains(field))
            {
                return true;
            }
            return false;
        }

        bool ChosenPieceIsValid(string piece) // chosen piece is a chess piece
        {
            if (Piece.PieceNames.Contains(piece))
            {
                return true;
            }
            return false;
        }
        */
        //to do end

        static Move StringToMove(string str)
        {
            string[] substrings = str.Split(" ");
            return new Move(substrings[0], substrings[1], substrings[2]);
        }

        public static void SetStartingBoard(Board board) // set pieces at starting position on the board
        {
            for (int i = 0; i < Board.boardSize; i++)
            {
                Fields[Board.Columns[i] + "2"].Content = new Pawn(true, Fields[Board.Columns[i] + "2"].Name);  // set white pawns
            }
            for (int i = 0; i < Board.boardSize; i++)
            {
                Fields[Board.Columns[i] + "7"].Content = new Pawn(false, Fields[Board.Columns[i] + "7"].Name);  // set black pawns
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
                    fieldnames[count] = Board.Columns[i] + Board.Rows[j];
                    count++;
                }
            }
            return fieldnames;
        }
    }     
}
