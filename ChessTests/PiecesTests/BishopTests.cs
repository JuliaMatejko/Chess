using Xunit;
using Chess.Model.Pieces;
using Chess.Model;
using System.Collections.Generic;

namespace ChessTests.PiecesTests
{
    public class BishopTests
    {
        // Testing bishop movement rules, when there are no other pieces on the board

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnA1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();                                                                       // Create a board
            Dictionary<string, Field> fields = Game.CreateFields(board);                                              // Create field dictionary
            var bishop = new Bishop(true, "a1");                                                                      // Create piece - bishop
            fields["a1"].Content = bishop;                                                                            // Put piece on the board
            HashSet<string> expectedPositions = new HashSet<string> { "b2", "c3", "d4", "e5", "f6", "g7", "h8" };     // A set of expected positions
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);               // Return available piece positions
            // Assert
            Assert.Equal(expectedPositions, actualPositions);                                                         // Chceck, if actual positions equals expected positions
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnF1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "f1");
            fields["f1"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "a6", "b5", "c4", "d3", "e2", "g2", "h3" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnH1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "h1");                
            fields["h1"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "g2", "f3", "e4", "d5", "c6", "b7", "a8" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnH4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "h4");
            fields["h4"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "e1", "e7", "d8", "f2", "f6", "g3", "g5" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnH8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "h8");
            fields["h8"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "g7", "f6", "e5", "d4", "c3", "b2", "a1" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnC8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "c8");
            fields["c8"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "b7", "a6", "d7", "e6", "f5", "g4", "h3" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnA8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "a8");
            fields["a8"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "h1", "g2", "f3", "e4", "d5", "c6", "b7" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnA5AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "a5");
            fields["a5"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "b4", "c3", "d2", "e1", "b6", "c7", "d8" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnF4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "f4");
            fields["f4"].Content = bishop;
            HashSet<string> expectedPositions = new HashSet<string> { "c1", "d2", "e3", "g5", "h6", "g3", "h2", "e5", "d6", "c7", "b8" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        // Testing bishop movement, when on the board are other pieces

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnF1AndBlockedByPlayersPieces__ReturnsEmptyPositionSet()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "f1");
            fields["f1"].Content = bishop;
            fields["e2"].Content = new Queen(true, "e2");        // Players pieces blocking bishop
            fields["g2"].Content = new Pawn(true, "g2");
            HashSet<string> expectedPositions = new HashSet<string> { };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnF1AndPartiallyBlockedByPlayersPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "f1");
            fields["f1"].Content = bishop;
            fields["e2"].Content = new Pawn(true, "e2");                                // One of the players pieces is blocking bishop
            HashSet<string> expectedPositions = new HashSet<string> { "g2", "h3" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnF1AndBlockedByOponentsPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "f1");
            fields["f1"].Content = bishop;
            fields["e2"].Content = new Bishop(false, "e2");        // Oponents pieces are blocking bishop but bishop can move on their position and capture them
            fields["g2"].Content = new Pawn(false, "g2");
            HashSet<string> expectedPositions = new HashSet<string> { "e2", "g2" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopOnF1AndPartialyBlockedByOponentsPieces__ReturnsCorrectPositions()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "f1");
            fields["f1"].Content = bishop;
            fields["c4"].Content = new Knight(false, "c4");
            fields["g2"].Content = new Pawn(false, "g2");
            HashSet<string> expectedPositions = new HashSet<string> { "e2", "d3", "g2", "c4" };
            // Act
            HashSet<string> actualPositions = bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Equal(expectedPositions, actualPositions);
        }

        // Test checking king with bishop

        [Fact]
        public void ReturnAvailablePieceMoves__BishopMovesOnB5AndChecksOponentsKing__OponentsKingIsChecked()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "b5");
            fields["e8"].Content = new King(false, "e8");
            fields["b5"].Content = bishop;
            // Act
            bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.True(GameState.BlackKingIsInCheck);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BishopMovesOnB5AndChecksOponentsKing__BishopIsAttackingTheKing()
        {
            // Arange                     
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var bishop = new Bishop(true, "b5");
            fields["e8"].Content = new King(false, "e8");
            fields["b5"].Content = bishop;
            // Act
            bishop.ReturnAvailablePieceMoves(bishop.Position, board);
            // Assert
            Assert.Contains(bishop, GameState.CurrentPlayerPiecesAttackingTheKing);
        }
    }
}
