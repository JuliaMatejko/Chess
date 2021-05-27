using Chess;
using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using Xunit;

namespace ChessTests.PiecesTests
{
    public class RookTests
    {
        // Testing rook movement rules, when there are no other pieces on the board

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();                                                                       // Create a board
            Dictionary<string, Field> fields = Game.CreateFields(board);                                              // Create field dictionary
            var rook = new Rook(true, "a1");                                                                          // Create piece - rook
            fields["a1"].Content = rook;                                                                              // Put piece on the board
            var expectedPositions = new HashSet<string> { "a2", "a3", "a4", "a5", "a6", "a7", "a8", 
                                                          "b1", "c1", "d1", "e1", "f1", "g1", "h1" };                 // A set of expected positions

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);                   // Return available piece positions

            Assert.Equal(expectedPositions, actualPositions);                                                         // Chceck, if actual positions equals expected positions
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnC1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "c1");
            fields["c1"].Content = rook;
            var expectedPositions = new HashSet<string> { "c2", "c3", "c4", "c5", "c6", "c7", "c8",
                                                          "a1", "b1", "d1", "e1", "f1", "g1", "h1" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnH1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "h1");
            fields["h1"].Content = rook;
            var expectedPositions = new HashSet<string> { "h2", "h3", "h4", "h5", "h6", "h7", "h8",
                                                          "a1", "b1", "c1", "d1", "e1", "f1", "g1" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnH5AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "h5");
            fields["h5"].Content = rook;
            var expectedPositions = new HashSet<string> { "h1", "h2", "h3", "h4", "h6", "h7", "h8",
                                                          "a5", "b5", "c5", "d5", "e5", "f5", "g5" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnH8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "h8");
            fields["h8"].Content = rook;
            var expectedPositions = new HashSet<string> { "h1", "h2", "h3", "h4", "h5", "h6", "h7",
                                                          "a8", "b8", "c8", "d8", "e8", "f8", "g8" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnD8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "d8");
            fields["d8"].Content = rook;
            var expectedPositions = new HashSet<string> { "d1", "d2", "d3", "d4", "d5", "d6", "d7",
                                                          "a8", "b8", "c8", "e8", "f8", "g8", "h8" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a8");
            fields["a8"].Content = rook;
            var expectedPositions = new HashSet<string> { "a1", "a2", "a3", "a4", "a5", "a6", "a7",
                                                          "b8", "c8", "d8", "e8", "f8", "g8", "h8" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA5AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a5");
            fields["a5"].Content = rook;
            var expectedPositions = new HashSet<string> { "a1", "a2", "a3", "a4", "a6", "a7", "a8",
                                                          "b5", "c5", "d5", "e5", "f5", "g5", "h5" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnC3AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "c3");
            fields["c3"].Content = rook;
            var expectedPositions = new HashSet<string> { "c1", "c2", "c4", "c5", "c6", "c7", "c8",
                                                          "a3", "b3", "d3", "e3", "f3", "g3", "h3" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        // Testing rook movement, when on the board are other pieces

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA1AndBlockedByPlayersPieces__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a1");
            fields["a1"].Content = rook;
            fields["b1"].Content = new Knight(true, "b1");        // Players pieces blocking rook
            fields["a2"].Content = new Pawn(true, "a2");
            HashSet<string> expectedPositions = new HashSet<string> { };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA1AndPartiallyBlockedByPlayersPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a1");
            fields["a1"].Content = rook;
            fields["a2"].Content = new Pawn(true, "a2");                                // One of the players pieces is blocking rook
            HashSet<string> expectedPositions = new HashSet<string> { "b1", "c1", "d1", "e1", "f1", "g1", "h1" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA1AndBlockedByOponentsPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a1");
            fields["a1"].Content = rook;
            fields["a2"].Content = new Bishop(false, "a2");        // Oponents pieces are blocking rook but rook can move on their position and capture them
            fields["b1"].Content = new Pawn(false, "b1");
            HashSet<string> expectedPositions = new HashSet<string> { "a2", "b1" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA1AndPartialyBlockedByOponentsPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a1");
            fields["a1"].Content = rook;
            fields["c1"].Content = new Knight(false, "c1");
            fields["a4"].Content = new Pawn(false, "a4");
            HashSet<string> expectedPositions = new HashSet<string> { "a2", "a3","a4", "b1", "c1" };

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        // Test rook interactions with oponents king

        [Fact]
        public void ReturnAvailablePieceMoves__RookOnA1AndCannotCaptureOponentsKing__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "a1");
            fields["a1"].Content = rook;
            var oponentsKingPosition = "c1";
            fields[oponentsKingPosition].Content = new King(false, oponentsKingPosition);

            HashSet<string> actualPositions = rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.DoesNotContain(oponentsKingPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookMovesOnE1AndChecksOponentsKing__OponentsKingIsChecked()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "e1");
            fields["e8"].Content = new King(false, "e8");
            fields["e1"].Content = rook;

            rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.True(Program.Game.BlackKingIsInCheck);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__RookMovesOnE1AndChecksOponentsKing__RookIsAttackingTheKing()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var rook = new Rook(true, "e1");
            fields["e8"].Content = new King(false, "e8");
            fields["e1"].Content = rook;

            rook.ReturnAvailablePieceMoves(rook.Position, board);

            Assert.Contains(rook, Program.Game.CurrentPlayerPiecesAttackingTheKing);
        }
    }
}
