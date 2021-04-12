using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    class MoveValidator // service which role is validating moves 
    { 
        public static bool MoveIsPossible(Piece piece, Move move)
        {
            if (PieceOnChosenPositionDoesExist(piece) && ChosenPieceIsCurrentPlayersPiece(piece))
            {
                return MoveIsCorrectPieceMove(piece, move);
            }
            return false;
        }
        
        static bool MoveIsCorrectPieceMove(Piece piece, Move move)
        {
            return piece.NextAvailablePositions.Contains(move.NewPosition);
        }
        /*
        static bool AbsolutePin(Move move) //! .... TO DO the pinned piece cannot legally move out of the line of attack (as moving it would expose the king to check).
        {
                if (true)
                {
                    return true;
                }
                return false;
            }
        */
        static bool ChosenPieceIsCurrentPlayersPiece(Piece piece)
        {
            if (piece.IsWhite && GameState.CurrentPlayer == GameState.Sides.White)
            {
                return true;
            }
            else if (!(piece.IsWhite) && GameState.CurrentPlayer == GameState.Sides.Black)
            {
                return true;
            }
            return false;
        }
        static bool PieceOnChosenPositionDoesExist(Piece piece)
        {
            return piece != null;
        }

        /*
            Sprawdzani ruchów:
            - przyjmij string (pw e2 e4) v
            - przekształć na obiekt Move ( piecename, currentPosition, newposition) v
            - prześlij do MoveValidatora
                - sprawdź czy piece o podanej nazwie i pozycji istnieje na planszy ( albo piece o podanych właściwościach Piece.Position, Piece.Name) v
                - sprawdź czy figura którą chce ruszyć gracz należy do niego v
                - sprawdź czy ruch jest legalny:
                   - tutaj wejdą różne metody zależne od figury ( będą dodawały możliwe ruchy do piece.NextAvailablePositions  lub odejmowały z niego)
                        ...
                - sprawdź czy piece nie musi chronić króla ( absolute pin) więc nie może się ruszyć)
                - sprawdź czy możliwy jest ruch na newposition ( czy piece.NextAvailablePositions zawiera newposition) (move is possible) END
                

         */
    }
}
