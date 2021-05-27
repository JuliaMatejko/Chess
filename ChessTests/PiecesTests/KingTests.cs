using Chess;
using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using Xunit;

namespace ChessTests.PiecesTests
{
    public class KingTests
    {
        // Testing king movement rules, when there are no other pieces on the board

        [Fact]
        public void ReturnAvailablePieceMoves__KingOnA1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();                                                                       // Create a board
            Dictionary<string, Field> fields = Game.CreateFields(board);                                              // Create field dictionary
            var king = new King(true, "a1");                                                                          // Create piece - king
            fields["a1"].Content = king;                                                                              // Put piece on the board
            HashSet<string> expectedPositions = new HashSet<string> { "a2", "b1", "b2" };                             // A set of expected positions

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);                   // Return available piece positions

            Assert.Equal(expectedPositions, actualPositions);                                                         // Check, if actual positions equals expected positions
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnF1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "f1");
            fields["f1"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "f2", "e1", "e2", "g1", "g2" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnH1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "h1");
            fields["h1"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "h2", "g1", "g2" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnH4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "h4");
            fields["h4"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "h3", "h5", "g3", "g4", "g5" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnH8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "h8");
            fields["h8"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "h7", "g8", "g7" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnC8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "c8");
            fields["c8"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "c7", "b7", "b8", "d7", "d8" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnA8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "a8");
            fields["a8"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "a7", "b8", "b7" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnA5AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "a5");
            fields["a5"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "a4", "a6", "b4", "b5", "b6" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnF4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "f4");
            fields["f4"].Content = king;
            HashSet<string> expectedPositions = new HashSet<string> { "f3", "f5", "e3", "e4", "e5", "g3", "g4", "g5" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        // Testing king movement, when on the board are other pieces

        [Fact]
        public void ReturnAvailablePieceMoves__KingOnE1AndBlockedByPlayersPieces__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            fields["e2"].Content = new Pawn(true, "e2");        // Players pieces blocking the king
            fields["d2"].Content = new Pawn(true, "d2");
            fields["f2"].Content = new Pawn(true, "f2");
            fields["d1"].Content = new Queen(true, "d1");
            fields["f1"].Content = new Bishop(true, "f1");
            HashSet<string> expectedPositions = new HashSet<string> { };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnE1AndPartiallyBlockedByPlayersPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            fields["e2"].Content = new Pawn(true, "e2");
            fields["d2"].Content = new Pawn(true, "d2");
            fields["f1"].Content = new Bishop(true, "f1");
            HashSet<string> expectedPositions = new HashSet<string> { "f2", "d1" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KingOnE1AndOponentsRookOnH2__ReturnsCorrectPositions()   // King cannot move on the squares attacked by oponents pieces.
        {                                                                                               // Oponents rook is attacking all squares of the second rank. King cannot move on d2, e2 or f2.
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            var rook = new Queen(false, "h2");
            fields["e2"].Content = rook;                                          
            rook.ReturnAvailablePieceMoves(rook.Position, Program.Game.Board);
            HashSet<string> expectedPositions = new HashSet<string> { "d1", "f1" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KingOnE1AndAttackedByTheQueenOnE2__ReturnsCorrectPositions()     // Oponents queen is attacking the king but the king can move on her position and capture the queen.
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            var queen = new Queen(false, "e2");
            fields["e2"].Content = queen;
            queen.ReturnAvailablePieceMoves(queen.Position, Program.Game.Board);
            HashSet<string> expectedPositions = new HashSet<string> { "e2" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__KingOnE1AndAttackedByTheQueenOnE2AndPawnOnF3__ReturnsEmptyPositionSet()      // Oponents queen is attacking the king.
        {                                                                                                                   // King cannot move on her position and capture the queen, because of the pawn protectig her.
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            var queen = new Queen(false, "e2");
            fields["e2"].Content = queen;
            var pawn = new Pawn(false, "f3");                                     
            fields["f3"].Content = pawn;
            pawn.ReturnAvailablePieceMoves(pawn.Position, board);
            queen.ReturnAvailablePieceMoves(queen.Position, board);
            HashSet<string> expectedPositions = new HashSet<string> { };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__CastlingKingSide__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            fields["h1"].Content = new Rook(true, "h1");
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            Program.Game.WhiteKingIsInCheck = false;
            var expectedPosition = "g1";

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Contains(expectedPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__CastlingQueenSide__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            fields["a1"].Content = new Rook(true, "a1");
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            Program.Game.WhiteKingIsInCheck = false;
            var expectedPosition = "c1";

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Contains(expectedPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__CastlingKingAndQueenSide__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            fields["a1"].Content = new Rook(true, "a1");
            fields["h1"].Content = new Rook(true, "h1");
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            Program.Game.WhiteKingIsInCheck = false;
            var expectedPositions = new HashSet<string> { "c1", "g1" };

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.Subset(actualPositions, expectedPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KingCannotCastleIfHasAlreadyDoneFirstMove__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            fields["a1"].Content = new Rook(true, "a1");
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            Program.Game.WhiteKingIsInCheck = false;
            king.IsFirstMove = false;
            var expectedPosition = "c1";

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.DoesNotContain(expectedPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KingCannotCastleWhenChecked__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            fields["a1"].Content = new Rook(true, "a1");
            var knight = new Knight(false, "f3");
            fields["f3"].Content = knight;
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            knight.ReturnAvailablePieceMoves(knight.Position, board);
            var expectedPosition = "c1";

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.DoesNotContain(expectedPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KingCannotCastleIfItWouldHaveToMoveThroughTheAttackedSquare__ReturnsCorrectPositions()   // Oponents knight is attacking b1 and d1 squares.
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            fields["a1"].Content = new Rook(true, "a1");
            var knight = new Knight(false, "c3");
            fields["c3"].Content = knight;
            var king = new King(true, "e1");
            fields["e1"].Content = king;
            knight.ReturnAvailablePieceMoves(knight.Position, board);
            var expectedPosition = "c1";

            HashSet<string> actualPositions = king.ReturnAvailablePieceMoves(king.Position, board);

            Assert.DoesNotContain(expectedPosition, actualPositions);
        }
    }
}
