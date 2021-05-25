using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using Xunit;

namespace ChessTests.PiecesTests
{
    public class QueenTests
    {
        // Testing queen movement rules, when there are no other pieces on the board

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnA1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();                                                                       // Create a board
            Dictionary<string, Field> fields = Game.CreateFields(board);                                              // Create field dictionary
            var queen = new Queen(true, "a1");                                                                        // Create piece - queen
            fields["a1"].Content = queen;                                                                             // Put piece on the board
            HashSet<string> expectedPositions = new HashSet<string> { "a2", "a3", "a4", "a5", "a6", "a7", "a8",       // A set of expected positions
                                                                      "b2", "c3", "d4", "e5", "f6", "g7", "h8", 
                                                                      "b1", "c1", "d1", "e1", "f1", "g1", "h1" };     

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);                 // Return available piece positions

            Assert.Equal(expectedPositions, actualPositions);                                                         // Check, if actual positions equals expected positions
        }

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnF1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "f1");
            fields["f1"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "a6", "b5", "c4", "d3", "e2", "g2", "h3",
                                                                      "a1", "b1", "c1", "d1", "e1", "g1", "h1",
                                                                      "f2", "f3", "f4", "f5", "f6", "f7", "f8" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnH1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "h1");
            fields["h1"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "g2", "f3", "e4", "d5", "c6", "b7", "a8",
                                                                      "h2", "h3", "h4", "h5", "h6", "h7", "h8",
                                                                      "a1", "b1", "c1", "d1", "e1", "f1", "g1" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnH4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "h4");
            fields["h4"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "e1", "e7", "d8", "f2", "f6", "g3", "g5",
                                                                      "h1", "h2", "h3", "h5", "h6", "h7", "h8",
                                                                      "a4", "b4", "c4", "d4", "e4", "f4", "g4" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnH8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "h8");
            fields["h8"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "g7", "f6", "e5", "d4", "c3", "b2", "a1",
                                                                      "h1", "h2", "h3", "h4", "h5", "h6", "h7",
                                                                      "a8", "b8", "c8", "d8", "e8", "f8", "g8" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnC8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "c8");
            fields["c8"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "b7", "a6", "d7", "e6", "f5", "g4", "h3",
                                                                      "c1", "c2", "c3", "c4", "c5", "c6", "c7",
                                                                      "a8", "b8", "d8", "e8", "f8", "g8", "h8" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnA8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "a8");
            fields["a8"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "h1", "g2", "f3", "e4", "d5", "c6", "b7",
                                                                      "a1", "a2", "a3", "a4", "a5", "a6", "a7",
                                                                      "b8", "c8", "d8", "e8", "f8", "g8", "h8" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnA5AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "a5");
            fields["a5"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "b4", "c3", "d2", "e1", "b6", "c7", "d8",
                                                                      "a1", "a2", "a3", "a4", "a6", "a7", "a8",
                                                                      "b5", "c5", "d5", "e5", "f5", "g5", "h5" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnF4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "f4");
            fields["f4"].Content = queen;
            HashSet<string> expectedPositions = new HashSet<string> { "c1", "d2", "e3", "g5", "h6",
                                                                      "g3", "h2", "e5", "d6","c7", "b8", 
                                                                      "f1", "f2", "f3", "f5", "f6", "f7", "f8", 
                                                                      "a4", "b4", "c4", "d4", "e4", "g4", "h4" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        // Testing queen movement, when on the board are other pieces

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnD1AndBlockedByPlayersPieces__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "d1");
            fields["d1"].Content = queen;
            fields["c2"].Content = new Pawn(true, "c2");        // Players pieces blocking the queen
            fields["d2"].Content = new Pawn(true, "d2");
            fields["e2"].Content = new Pawn(true, "e2");
            fields["c1"].Content = new Bishop(true, "c1");
            fields["e1"].Content = new King(true, "e1");
            HashSet<string> expectedPositions = new HashSet<string> { };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnD1AndPartiallyBlockedByPlayersPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "d1");
            fields["d1"].Content = queen;
            fields["c2"].Content = new Pawn(true, "c2");        // Players piece blocking the queen
            fields["d4"].Content = new Pawn(true, "d4");        // Players piece partially blocking the queen
            fields["e3"].Content = new Pawn(true, "e3");        // Players piece is not blocking the queen
            fields["c1"].Content = new Bishop(true, "c1");      // Players piece blocking the queen
            fields["e1"].Content = new King(true, "e1");        // Players piece blocking the queen
            HashSet<string> expectedPositions = new HashSet<string> { "d2", "d3", "e2", "f3", "g4", "h5" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnD1AndBlockedByOponentsPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "d1");
            fields["d1"].Content = queen;
            fields["c2"].Content = new Pawn(false, "c2");       // Oponents pieces are blocking the queen but the queen can move on their position and capture them
            fields["d2"].Content = new Pawn(false, "d2");
            fields["e2"].Content = new Pawn(false, "e2");
            fields["c1"].Content = new Bishop(false, "c1");
            fields["e1"].Content = new Knight(false, "e1");
            HashSet<string> expectedPositions = new HashSet<string> { "c2", "d2", "e2", "c1", "e1" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnD1AndPartialyBlockedByOponentsPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "d1");
            fields["d1"].Content = queen;
            fields["c2"].Content = new Pawn(false, "c2");        // Oponents piece blocking the queen
            fields["d4"].Content = new Pawn(false, "d4");        // Oponents piece partially blocking the queen
            fields["e3"].Content = new Pawn(false, "e3");        // Oponents piece is not blocking the queen
            fields["c1"].Content = new Bishop(false, "c1");      // Oponents piece blocking the queen
            fields["e1"].Content = new Knight(false, "e1");        // Oponents piece blocking the queen
            HashSet<string> expectedPositions = new HashSet<string> { "c2", "d2", "d3", "d4", "e2", 
                                                                      "f3", "g4", "h5", "c1", "e1" };

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        // Test queen interactions with oponents king 

        [Fact]
        public void ReturnAvailablePieceMoves__QueenOnE1AndCannotCaptureOponentsKing__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "e1");
            fields["e1"].Content = queen;
            var oponentsKingPosition = "e8";
            fields[oponentsKingPosition].Content = new King(false, oponentsKingPosition);

            HashSet<string> actualPositions = queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.DoesNotContain(oponentsKingPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__QueenMovesOnE1AndChecksOponentsKing__OponentsKingIsChecked()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "e1");
            fields["e8"].Content = new King(false, "e8");
            fields["e1"].Content = queen;

            queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.True(GameState.BlackKingIsInCheck);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__QueenMovesOnE1AndChecksOponentsKing__QueenIsAttackingTheKing()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var queen = new Queen(true, "e1");
            fields["e8"].Content = new King(false, "e8");
            fields["e1"].Content = queen;

            queen.ReturnAvailablePieceMoves(queen.Position, board);

            Assert.Contains(queen, GameState.CurrentPlayerPiecesAttackingTheKing);
        }
    }
}
