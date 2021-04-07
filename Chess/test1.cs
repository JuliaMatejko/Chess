using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class test1
    {
        /* move methodes //nie można wykonać ruchu na to samo pole todo

            void MakeAMove()
            {
                Console.Write(" Choose a piece and a field: ");
                string chosenMove = Console.ReadLine();

                (string piece, string field) move = StringToTuple(chosenMove, " ");// change to vallue key? todo

                while (!MoveIsPossible(move))
                {
                    Console.WriteLine($" It's not a valid move. Choose a differnt piece or/and field: "); //to do --> more specific?
                    chosenMove = Console.ReadLine();
                }

                board[0][move.field] = move.piece;

                //! .... TO DO

                // move == 
                //1. skopiuj wartość do nowego pola
                //2. usuń wartość w starym polu
                //
                //iterancja po kluczach szukając 'field' np. a1         if a than 0 switch ib than 1
                //OrderedDictonary<string, OrderedDictionary<string, string>> chessboard --> Stworzyć taki typ?
                //board["A"]["1"]
                //board[0][move.field] = move.piece;


                Console.WriteLine("You've made a move!");//test-delete
            }

            bool MoveIsPossible((string, string) move)
            {
                if (MoveIsValid(move) && MoveIsLegal(move))
                {
                    return true;
                }
                return false;
            }

            bool MoveIsLegal((string, string) move)
            {
                if (MoveIsPrinciplePieceMove(move) && !ChosenPieceProtectsKingCheck(move))
                {
                    return true;
                }
                return false;
            }

            bool MoveIsPrinciplePieceMove((string piece, string field) move) //! .... TO DO
            {
                if (availablePieceMoves.Contains(move.field))
                {
                    return true;
                }
                return false;
                //return true;
            }

            void ReturnAvailablePieceMoves((string piece, string field) move)
            {
                List<string> availablePieceMoves = new List<string>();
                //pawn
                //MoveOneForward();
                //MoveTwoForward();


            }

            bool ChosenPieceProtectsKingCheck((string piece, string field) move) //! .... TO DO
            {
                /*if (true)
                {
                    return true;
                }
                return false;
                return false;
            }

        bool MoveIsValid((string piece, string field) move)
        {
            if (ChosenPieceIsValid(Convert.ToString(move.piece)) && ChosenFieldIsValid(Convert.ToString(move.field)))
            {
                return true;
            }
            return false;
        }

        bool ChosenFieldIsValid(string field) // chosen field is on the board
        {
            if (board.fieldNames.Contains(field))
            {
                return true;
            }
            return false;
        }

        bool ChosenPieceIsValid(string piece) // chosen piece is a chess piece
        {
            if (pieceNames.Contains(piece))
            {
                return true;
            }
            return false;
        }

        (string, string) StringToTuple(string str, string separator) // helper method to convert string format move to tuple
        {
            string[] substrings = str.Split(separator);
            (string, string) tuple = (substrings[0], substrings[1]);

            return tuple;
        }*/
    }
}

