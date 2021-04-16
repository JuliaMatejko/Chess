﻿using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    abstract class Piece
    {
        public bool IsWhite { get; set; }
        public string Name { get; set; }    // piece representation on the board/ in the future: przekazywane do kontrolera i wyswietlane odpowiednio w warstw. prezentacji?
        public string Position { get; set; } //field.name board[][].Name
        public List<string> NextAvailablePositions
        {
            set => nextAvailablePositions = value;
            get => ReturnAvailablePieceMoves(Position, Game.board);    // fields available for piece to move in ongoing round
        }
        private List<string> nextAvailablePositions;
        public static string[] PieceNames => new string[] { "pw", "pb", "Rw", "Rb", "kw", "kb", "Bw", "Bb", "Qw", "Qb", "Kw", "Kb" };

        protected List<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            int fileIndex = Array.IndexOf(Board.Files, Convert.ToString(currentposition[0]));
            int rankIndex = Array.IndexOf(Board.Ranks, Convert.ToString(currentposition[1]));
            Field newField = null;
            List<string> positions = new List<string>();
            positions.AddRange(ReturnCorrectPieceMoves(fileIndex, rankIndex, newField, board, positions));
            /* foreach (var item in positions)
             {
                 Console.WriteLine(item);
             }*/
            return positions;

        }

        protected abstract List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions);

        protected List<string> MovePiece(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            
            MoveForward();
            MoveBack();
            MoveLeft();
            MoveRight();

            MoveRightForward();
            MoveLeftBackwards();
            MoveLeftForward();
            MoveRightBackwards();

            void MoveForward()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                //bool movesInBoardBoundaries = IsWhite ? rank < Board.boardSize - 1 : rank > 0;    nie działa , problem z rank? -> wartość się nie zmienia? sprawdź
                if (IsWhite)
                {
                    while (rank < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(0, 1, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (rank > 0 && canMove)
                    {
                        MoveOne(0, 1, ref file, ref rank, ref canMove);
                    }
                }
               // while (movesInBoardBoundaries && canMove)
               // {
               //     MoveOne(0, 1, ref file, ref rank, ref canMove);
                //}
            }
            
            void MoveBack()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                //bool movesInBoardBoundaries = IsWhite ? rank > 0 : rank < (Board.boardSize - 1);          nie działa , problem z rank? -> wartość się nie zmienia? sprawdź

                if (IsWhite)
                {
                    while (rank > 0 && canMove)
                    {
                        MoveOne(0, -1, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (rank < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(0, -1, ref file, ref rank, ref canMove);
                    }
                }

                //while (movesInBoardBoundaries && canMove)
                //{
                   // MoveOne(0, -1, ref file, ref rank, ref canMove);
                //}
            }

            void MoveLeft()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                if (IsWhite)
                {
                    while (file > 0 && canMove)
                    {
                        MoveOne(-1, 0, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (file < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(-1, 0, ref file, ref rank, ref canMove);
                    }
                }
            }

            void MoveRight()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                if (IsWhite)
                {
                    while (file < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(1, 0, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (file > 0 && canMove)
                    {
                        MoveOne(1, 0, ref file, ref rank, ref canMove);
                    }
                }
            }

            void MoveRightForward()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                if (IsWhite)
                {
                    while (file < Board.boardSize - 1 && rank < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(1, 1, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (file > 0 && rank > 0 && canMove)
                    {
                        MoveOne(1, 1, ref file, ref rank, ref canMove);
                    }
                }
            }

            void MoveLeftBackwards()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                if (IsWhite)
                {
                    while (file > 0 && rank > 0 && canMove)   
                    {
                        MoveOne(-1, -1, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (file < Board.boardSize - 1 && rank < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(-1, -1, ref file, ref rank, ref canMove);
                    }
                }
            }

            void MoveLeftForward()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                if (IsWhite)
                {
                    while (file > 0 && rank < Board.boardSize - 1 && canMove)
                    {
                        MoveOne(-1, 1, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (file < Board.boardSize - 1 && rank > 0 && canMove)
                    {
                        MoveOne(-1, 1, ref file, ref rank, ref canMove);
                    }
                }
            }

            void MoveRightBackwards()
            {
                bool canMove = true;
                int rank = rankIndex;
                int file = fileIndex;
                if (IsWhite)
                {
                    while (file < Board.boardSize - 1 && rank > 0 && canMove)
                    {
                        MoveOne(1, -1, ref file, ref rank, ref canMove);
                    }
                }
                else
                {
                    while (file > 0 && rank < Board.boardSize - 1 && canMove)  
                    {
                        MoveOne(1, -1, ref file, ref rank, ref canMove);
                    }
                }
            }

            void MoveOne(int x_white, int y_white, ref int file, ref int rank, ref bool canMove) // przyjmuje argumenty(wektor), odpowiednio interpretuje dla koloru gracza, pobiera pole z planszy i sprawdza je: 1. czy jest wolne, jeśli tak to dodaje jego koordynaty do listy dostępnych pól i funkcja kontynuuje swoje działanie dla kolejnego pola przesuniętego o ten sam wektor
            {
                int x = IsWhite ? x_white : -x_white;
                int y = IsWhite ? y_white : -y_white;

                newField = board[file + x][rank + y];
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[file + x] + Board.Ranks[rank + y]);
                    file += x;
                    rank += y;
                }
                else                                                                                                    //if (newField.Content != null && newField.Content.GetType() != typeof(King)) //newField.Content != null 
                {
                    bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                    if (newField.Content.GetType() != typeof(King))
                    { 
                        if (z)
                        {
                            positions.Add(Board.Files[file + x] + Board.Ranks[rank + y]);
                        }
                    }
                    else                                                                                                        // newField.Content.GetType() == typeof(King)
                    {
                        if (z)
                        {
                            //set king in check?
                        }
                    }
                    canMove = false;
                }
            }

            return positions;
        }
    }
}
