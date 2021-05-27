using Chess.Controller;
using Chess.Model.Pieces;
using Chess.View;
using System;
using System.Collections.Generic;

namespace Chess.Model
{
    class Game
    {
        public enum Sides
        {
            White = 1,
            Black = 0
        }
        public Sides CurrentPlayer { get; set; } = Sides.White;
        public Pawn WhitePawnThatCanBeTakenByEnPassantMove { get; set; }
        public Pawn BlackPawnThatCanBeTakenByEnPassantMove { get; set; }
        public King WhiteKing { get; set; }
        public King BlackKing { get; set; }
        public bool WhiteKingIsInCheck { get; set; } = false;
        public bool BlackKingIsInCheck { get; set; } = false;
        public bool CurrentPlayerKingIsInCheck => CurrentPlayer == Sides.White ? WhiteKingIsInCheck : BlackKingIsInCheck;
        public bool IsAWin => WinConditionMet();
        public bool IsADraw => DrawConditionMet();
        public bool PlayerResigned { get; set; } = false;        // TO DO someday: implementation may change in the future so that the resignation is not only possible for the current player
        public bool PlayerOfferedADraw { get; set; } = false;    // TO DO someday: implementation may change in the future so that offering a draw is not only possible for the current player
        public bool PlayersAgreedToADraw { get; set; } = false;
        public bool IsAStalemate => StalemateOccured();
        public List<Piece> CurrentPlayerPiecesAttackingTheKing { get; set; } = new List<Piece>();
        public bool IsACheckmate => CheckmateOccured();

        public Board Board { get; } = Board.CreateABoard();
        public Dictionary<string, Field> Fields
        { 
            get => CreateFields(Board);
            set => _fields = value; 
        }
        private Dictionary<string, Field> _fields;


        public void StartGame()
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

        private static void SetStartingBoard() // set pieces on the starting position on the board
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                Program.Game.Fields[Board.Files[i] + "2"].Content = new Pawn(true, Board.Files[i] + "2");  // set white pawns
            }
            for (var i = 0; i < Board.BoardSize; i++)
            {
                Program.Game.Fields[Board.Files[i] + "7"].Content = new Pawn(false, Board.Files[i] + "7");  // set black pawns 
            }
            Program.Game.Fields["a1"].Content = new Rook(true, "a1");        // set white rooks
            Program.Game.Fields["h1"].Content = new Rook(true, "h1");
            Program.Game.Fields["a8"].Content = new Rook(false, "a8");       // set black rooks
            Program.Game.Fields["h8"].Content = new Rook(false, "h8");
            Program.Game.Fields["b1"].Content = new Knight(true, "b1");      // set white knights
            Program.Game.Fields["g1"].Content = new Knight(true, "g1");
            Program.Game.Fields["b8"].Content = new Knight(false, "b8");     // set black knights
            Program.Game.Fields["g8"].Content = new Knight(false, "g8");
            Program.Game.Fields["c1"].Content = new Bishop(true, "c1");      // set white bishops
            Program.Game.Fields["f1"].Content = new Bishop(true, "f1");
            Program.Game.Fields["c8"].Content = new Bishop(false, "c8");     // set black bishops
            Program.Game.Fields["f8"].Content = new Bishop(false, "f8");
            Program.Game.Fields["d1"].Content = new Queen(true, "d1");       // set white queen
            Program.Game.Fields["d8"].Content = new Queen(false, "d8");      // set black queen*/
            Program.Game.Fields["e1"].Content = new King(true, "e1");        // set white king
            Program.Game.WhiteKing = (King)Program.Game.Fields["e1"].Content;
            Program.Game.Fields["e8"].Content = new King(false, "e8");       // set black king
            Program.Game.BlackKing = (King)Program.Game.Fields["e8"].Content;
        }

        private void ChangeTurns()
        {
            CurrentPlayer = CurrentPlayer == Sides.White ? CurrentPlayer = Sides.Black
                                                         : CurrentPlayer = Sides.White;
        }

        private void ResetEnPassantFlag()
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

        private void ResetCurrentPiecesAttackingTheKing()
        {
            Program.Game.CurrentPlayerPiecesAttackingTheKing.Clear();
        }

        public void ResetKingCheckFlag()
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

        private bool WinConditionMet()
        {
            return IsACheckmate || PlayerResigned;
        }

        private bool CheckmateOccured()
        {
            bool oponentsKingIsInCheck = CurrentPlayer == Sides.White ? BlackKingIsInCheck : WhiteKingIsInCheck;
            if (oponentsKingIsInCheck)
            {
                bool oponentsKingIsDoubleChecked = CurrentPlayerPiecesAttackingTheKing.Count > 1;
                if (oponentsKingIsDoubleChecked && !KingCanMoveAway())
                {
                    return true;
                }
                else if (!KingCanMoveAway())
                {
                    if (CurrentPlayerPiecesAttackingTheKing[0].GetType() == typeof(IDiagonallyMovingPiece)
                        || CurrentPlayerPiecesAttackingTheKing[0].GetType() == typeof(IHorizontallyAndVerticallyMovingPiece))
                    {
                        if (!CheckCanBeBlockedOrAttackingPieceCanBeCaptured())
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (!AttackingPieceCanBeCaptured())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

            bool KingCanMoveAway()
            {
                int oponentsKingAvailablePositionsCount = CurrentPlayer == Sides.White ? BlackKing.NextAvailablePositions.Count
                                                                                       : WhiteKing.NextAvailablePositions.Count;
                return oponentsKingAvailablePositionsCount != 0;
            }

            bool AttackingPieceCanBeCaptured()
            {
                for (var i = 0; i < Board.BoardSize; i++)
                {
                    for (var j = 0; j < Board.BoardSize; j++)
                    {
                        Piece piece = Program.Game.Board[i][j].Content;
                        if (piece != null)
                        {
                            bool isOponentsPiece = CurrentPlayer == Sides.White ? !piece.IsWhite : piece.IsWhite;
                            if (isOponentsPiece)
                            {
                                if (piece.NextAvailablePositions.Contains(CurrentPlayerPiecesAttackingTheKing[0].Position))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            bool CheckCanBeBlockedOrAttackingPieceCanBeCaptured() // Return positions that are blocking check
            {
                List<string> blockingPositions = new List<string>();
                int currentPlayersPieceFile = Array.IndexOf(Board.Files, CurrentPlayerPiecesAttackingTheKing[0].Position.Substring(0, 1));
                int currentPlayersPieceRank = Array.IndexOf(Board.Ranks, CurrentPlayerPiecesAttackingTheKing[0].Position.Substring(1, 1));
                King oponentsKing = CurrentPlayer == Sides.White ? BlackKing : WhiteKing;
                int oponentsKingFile = Array.IndexOf(Board.Files, oponentsKing.Position.Substring(0, 1));
                int oponentsKingRank = Array.IndexOf(Board.Ranks, oponentsKing.Position.Substring(1, 1));

                int x = oponentsKingFile == currentPlayersPieceFile ? 0 : (oponentsKingFile > currentPlayersPieceFile ? 1 : -1);    // Check if both pieces are on the same file --> A rook or a queen is an attacking piece
                int y = oponentsKingRank == currentPlayersPieceRank ? 0 : (oponentsKingRank > currentPlayersPieceRank ? 1 : -1);    // Check if both pieces are on the same rank --> A rook or a queen is an attacking piece
                int file = currentPlayersPieceFile + x;                                                                             // Otherwise, pieces are on the different file and rank-- > A bishop or a queen is an attacking piece
                int rank = currentPlayersPieceRank + y;

                bool canMoveInBetween = y == 0 ? (oponentsKingFile > currentPlayersPieceFile ? file < oponentsKingFile : file > oponentsKingFile)
                                                : (oponentsKingRank > currentPlayersPieceRank ? rank < oponentsKingRank : rank > oponentsKingRank);
                while (canMoveInBetween)
                {
                    blockingPositions.Add(Board.Files[file] + Board.Ranks[rank]);
                    file += x;
                    rank += y;
                }

                for (int i = 0; i < Board.BoardSize; i++)
                {
                    for (int j = 0; j < Board.BoardSize; j++)
                    {
                        Piece piece = Program.Game.Board[i][j].Content;
                        if (piece != null)
                        {
                            bool isOponentsPiece = CurrentPlayer == Sides.White ? !piece.IsWhite : piece.IsWhite;
                            if (isOponentsPiece)
                            {
                                if (piece.NextAvailablePositions.Contains(CurrentPlayerPiecesAttackingTheKing[0].Position))    // Check if any of the oponents pieces can capture attacking piece
                                {
                                    return true;
                                }
                                foreach (string position in blockingPositions)                                                 // Check if any of the oponents pieces can move on the blocking position
                                {
                                    if (piece.NextAvailablePositions.Contains(position))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

        private bool DrawConditionMet()
        {
            /*   TODO someday:
             if (IsADeadPosition()) // automatic draw
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
             */
            return PlayersAgreedToADraw || IsAStalemate;
        }

        private bool StalemateOccured()
        {
            for (int i = 0; i < Board.BoardSize; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Piece piece = Program.Game.Board[i][j].Content;
                    if (piece != null)
                    {
                        bool isOponentsPiece = CurrentPlayer == Sides.White ? !piece.IsWhite : piece.IsWhite;
                        if (isOponentsPiece && piece.NextAvailablePositions.Count != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /*    TODO someday:
        static bool IsADeadPosition()
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
        */
    }
}
