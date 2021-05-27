using Chess;
using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using Xunit;

namespace ChessTests.PiecesTests
{
    public class KnightTests
    {
        // Testing knight movement rules, when there are no other pieces on the board

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnA1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();                                                                       // Create a board
            Dictionary<string, Field> fields = Game.CreateFields(board);                                              // Create field dictionary
            var knight = new Knight(true, "a1");                                                                      // Create piece - knight
            fields["a1"].Content = knight;                                                                            // Put piece on the board
            HashSet<string> expectedPositions = new HashSet<string> { "b3", "c2" };                                   // A set of expected positions

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);               // Return available piece positions

            Assert.Equal(expectedPositions, actualPositions);                                                         // Check, if actual positions equals expected positions
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnC1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "c1");
            fields["c1"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "a2", "b3", "d3", "e2" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnH1AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "h1");
            fields["h1"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "f2", "g3" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnH4AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "h4");
            fields["h4"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "g2", "f3", "f5", "g6" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnH8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "h8");
            fields["h8"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "g6", "f7" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnG8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "g8");
            fields["g8"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "h6", "f6", "e7" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnA8AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "a8");
            fields["a8"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "b6", "c7" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnA5AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "a5");
            fields["a5"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "b7", "c6", "c4", "b3" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnB3AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "b3");
            fields["b3"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "a5", "c5", "d4", "d2", "c1", "a1" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnC3AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "c3");
            fields["c3"].Content = knight;
            HashSet<string> expectedPositions = new HashSet<string> { "a2", "b1", "d1", "e2", "e4", "d5", "b5", "a4" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        // Testing knight movement, when on the board are other pieces

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnA1AndPlayersPiecesOnB3AndC2__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "a1");
            fields["a1"].Content = knight;
            fields["c2"].Content = new Pawn(true, "c2");        // Knight cannot move on fields with players pieces
            fields["b3"].Content = new Bishop(true, "b3");
            HashSet<string> expectedPositions = new HashSet<string> { };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnA1AndOtherPiecesOnA2AndB1AndB2__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "a1");
            fields["a1"].Content = knight;
            fields["a2"].Content = new Pawn(true, "a2");        // Knight jumps over players pieces
            fields["b1"].Content = new Knight(false, "b1");     // Knight jumps over oponents pieces
            fields["b2"].Content = new Bishop(false, "b2");
            HashSet<string> expectedPositions = new HashSet<string> { "b3", "c2" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnA1AndIsAttackingOponentsPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "a1");
            fields["a1"].Content = knight;
            fields["b3"].Content = new Pawn(false, "b3");
            fields["c2"].Content = new Bishop(false, "c2");
            HashSet<string> expectedPositions = new HashSet<string> { "b3", "c2" };

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        // Test knight interactions with oponents king 

        [Fact]
        public void ReturnAvailablePieceMoves__KnightOnC3AndCannotCaptureOponentsKing__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "c3");
            fields["c3"].Content = knight;
            var oponentsKingPosition = "d5";
            fields[oponentsKingPosition].Content = new King(false, oponentsKingPosition);

            HashSet<string> actualPositions = knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.DoesNotContain(oponentsKingPosition, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightMovesOnC3AndChecksOponentsKing__OponentsKingIsChecked()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "c3");
            fields["d5"].Content = new King(false, "d5");
            fields["c3"].Content = knight;

            knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.True(Program.Game.BlackKingIsInCheck);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__KnightMovesOnC3AndChecksOponentsKing__KnightIsAttackingTheKing()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var knight = new Knight(true, "c3");
            fields["d5"].Content = new King(false, "d5");
            fields["c3"].Content = knight;

            knight.ReturnAvailablePieceMoves(knight.Position, board);

            Assert.Contains(knight, Program.Game.CurrentPlayerPiecesAttackingTheKing);
        }
    }
}
