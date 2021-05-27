using Chess;
using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using Xunit;

namespace ChessTests.PiecesTests
{
    public class PawnTests
    {
        // Testing pawn movement rules, when there are no other pieces on the board

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnA2AndNoOtherPieces__ReturnsCorrectPositions()                    // It is first move for this pawn, so it can move one or two squares forward
        {
            Board board = Board.CreateABoard();                                                                       // Create a board
            Dictionary<string, Field> fields = Game.CreateFields(board);                                              // Create field dictionary
            var pawn = new Pawn(true, "a2");                                                                          // Create piece - pawn
            fields["a2"].Content = pawn;                                                                              // Put piece on the board
            var expectedPositions = new HashSet<string> { "a3", "a4" };                                               // A set of expected positions

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);                   // Return available piece positions

            Assert.Equal(expectedPositions, actualPositions);                                                         // Chceck, if actual positions equals expected positions
        }

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnA3AndNoOtherPieces__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "a3")
            {
                IsFirstMove = false                               // It is not first move for this pawn, if it is on a3
            };
            fields["a3"].Content = pawn;
            var expectedPositions = new HashSet<string> { "a4" };

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        // Testing pawn movement, when on the board are other pieces

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnA3AndBlockedByPlayersPiece__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "a3")
            {
                IsFirstMove = false
            };
            fields["a3"].Content = pawn;
            fields["a4"].Content = new Bishop(true, "a4");                      // Players piece blocking pawn
            HashSet<string> expectedPositions = new HashSet<string> { };

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnA2AndBlockedByPlayersPiece__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "a2");
            fields["a2"].Content = pawn;
            fields["a3"].Content = new Knight(true, "a3");                      // Players piece blocking pawn
            HashSet<string> expectedPositions = new HashSet<string> { };        // Although, it is a first move for this pawn, it cannot go forward two squares on a4 jumping over other piece

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnA3AndOponentsPieceOnB4__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "a3")
            {
                IsFirstMove = false
            };
            fields["a3"].Content = pawn;
            var oponentsPiecePosition = "b4";
            fields[oponentsPiecePosition].Content = new Bishop(false, oponentsPiecePosition);                     

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(oponentsPiecePosition, actualPositions);         // Oponents piece can be captured
        }

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnH3AndOponentsPieceOnG4__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "h3")
            {
                IsFirstMove = false
            };
            fields["h3"].Content = pawn;
            var oponentsPiecePosition = "g4";
            fields[oponentsPiecePosition].Content = new Bishop(false, oponentsPiecePosition);

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(oponentsPiecePosition, actualPositions);         // Oponents piece can be captured
        }

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnG3AndOponentsPiecesOnF4AndG4AndH4__ReturnsEmptyPositionSet()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "g3")
            {
                IsFirstMove = false
            };
            fields["g3"].Content = pawn;
            fields["g4"].Content = new Pawn(false, "g4");
            fields["f4"].Content = new Bishop(false, "f4");
            fields["h4"].Content = new Pawn(false, "h4");
            HashSet<string> expectedPositions = new HashSet<string> { "f4" , "h4" };                    // Oponents pieces on f4 and h4 can be captured.
                                                                                                        // Oponents piece on g4 is blocking pawn movement on this square and cannot be captured.
            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Equal(expectedPositions, actualPositions);
        }

        [Fact]
        public void ReturnAvailablePieceMoves__WhitePawnOnA5AndOponentsPawnOnB5AfterThisPawnFirstMove__ReturnsCorrectPositions()    // Tests En passant move on the right side for a white pawn
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "a5")
            {
                IsFirstMove = false
            };
            fields["a5"].Content = pawn;
            var oponentsPawnPosition = "b5";
            var oponentsPawn = new Pawn(false, oponentsPawnPosition)
            {
                CanBeTakenByEnPassantMove = true
            };
            Program.Game.BlackPawnThatCanBeTakenByEnPassantMove = oponentsPawn;
            fields[oponentsPawnPosition].Content = oponentsPawn;
            var pawnPositionAfterEnPassantMove = "b6";

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawnPositionAfterEnPassantMove, actualPositions);         // Oponents piece can be captured en passant with pawn moving on b6
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BlackPawnOnH4AndOponentsPawnOnG4AfterThisPawnFirstMove__ReturnsCorrectPositions()    // Tests En passant move on the right side for a black pawn
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(false, "h4")
            {
                IsFirstMove = false
            };
            fields["h4"].Content = pawn;
            var oponentsPawnPosition = "g4";
            var oponentsPawn = new Pawn(true, oponentsPawnPosition)
            {
                CanBeTakenByEnPassantMove = true
            };
            Program.Game.WhitePawnThatCanBeTakenByEnPassantMove = oponentsPawn;
            fields[oponentsPawnPosition].Content = oponentsPawn;
            var pawnPositionAfterEnPassantMove = "g3";

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawnPositionAfterEnPassantMove, actualPositions);         // Oponents piece can be captured en passant with pawn moving on g3
        }

        [Fact]
        public void ReturnAvailablePieceMoves__WhitePawnOnH5AndOponentsPawnOnG5AfterThisPawnFirstMove__ReturnsCorrectPositions()    // Tests En passant move on the left side for a white pawn
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "h5")
            {
                IsFirstMove = false
            };
            fields["h5"].Content = pawn;
            var oponentsPawnPosition = "g5";
            var oponentsPawn = new Pawn(false, oponentsPawnPosition)
            {
                CanBeTakenByEnPassantMove = true
            };
            Program.Game.BlackPawnThatCanBeTakenByEnPassantMove = oponentsPawn;
            fields[oponentsPawnPosition].Content = oponentsPawn;
            var pawnPositionAfterEnPassantMove = "g6";

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawnPositionAfterEnPassantMove, actualPositions);         // Oponents piece can be captured en passant with pawn moving on g6
        }

        [Fact]
        public void ReturnAvailablePieceMoves__BlackPawnOnA4AndOponentsPawnOnB4AfterThisPawnFirstMove__ReturnsCorrectPositions()    // Tests En passant move on the left side for a black pawn
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(false, "a4")
            {
                IsFirstMove = false
            };
            fields["a4"].Content = pawn;
            var oponentsPawnPosition = "b4";
            var oponentsPawn = new Pawn(true, oponentsPawnPosition)
            {
                CanBeTakenByEnPassantMove = true
            };
            Program.Game.WhitePawnThatCanBeTakenByEnPassantMove = oponentsPawn;
            fields[oponentsPawnPosition].Content = oponentsPawn;
            var pawnPositionAfterEnPassantMove = "b3";

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawnPositionAfterEnPassantMove, actualPositions);         // Oponents piece can be captured en passant with pawn moving on b3
        }

        [Fact]
        public void ReturnAvailablePieceMoves__WhitePawnOnD5AndOponentsPawnOnC5AfterThisPawnsFirstMove__ReturnsCorrectPositions()    // Tests En passant move on the left side for a white pawn
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "d5")
            {
                IsFirstMove = false
            };
            fields["d5"].Content = pawn;
            var oponentsPawnPosition = "c5";
            var oponentsPawn = new Pawn(false, oponentsPawnPosition)
            {
                CanBeTakenByEnPassantMove = true
            };
            Program.Game.BlackPawnThatCanBeTakenByEnPassantMove = oponentsPawn;
            fields[oponentsPawnPosition].Content = oponentsPawn;
            var pawnPositionAfterEnPassantMove = "c6";

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawnPositionAfterEnPassantMove, actualPositions);         // Oponents piece can be captured en passant with pawn moving on c6
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__BlackPawnOnF4AndOponentsPawnOnE4AfterThisPawnsFirstMove__ReturnsCorrectPositions()    // Tests En passant move on the right side for a black pawn
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(false, "f4")
            {
                IsFirstMove = false
            };
            fields["f4"].Content = pawn;
            var oponentsPawnPosition = "e4";
            var oponentsPawn = new Pawn(true, oponentsPawnPosition)
            {
                CanBeTakenByEnPassantMove = true
            };
            Program.Game.WhitePawnThatCanBeTakenByEnPassantMove = oponentsPawn;
            fields[oponentsPawnPosition].Content = oponentsPawn;
            var pawnPositionAfterEnPassantMove = "e3";

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawnPositionAfterEnPassantMove, actualPositions);         // Oponents piece can be captured en passant with pawn moving on e3
        }

        // Test pawn interactions with oponents king

        [Fact]
        public void ReturnAvailablePieceMoves__PawnOnD2AndCannotCaptureOponentsKing__ReturnsCorrectPositions()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "d2");
            fields["d2"].Content = pawn;
            var oponentsKingPosition = "c3";
            fields[oponentsKingPosition].Content = new King(false, oponentsKingPosition);

            HashSet<string> actualPositions = pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.DoesNotContain(oponentsKingPosition, actualPositions);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__PawnMovesOnF4AndChecksOponentsKing__OponentsKingIsChecked()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "f4");
            fields["g5"].Content = new King(false, "g5");
            fields["f4"].Content = pawn;

            pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.True(Program.Game.BlackKingIsInCheck);
        }
        
        [Fact]
        public void ReturnAvailablePieceMoves__PawnMovesOnF4AndChecksOponentsKing__PawnIsAttackingTheKing()
        {
            Board board = Board.CreateABoard();
            Dictionary<string, Field> fields = Game.CreateFields(board);
            var pawn = new Pawn(true, "f4");
            fields["g5"].Content = new King(false, "g5");
            fields["f4"].Content = pawn;

            pawn.ReturnAvailablePieceMoves(pawn.Position, board);

            Assert.Contains(pawn, Program.Game.CurrentPlayerPiecesAttackingTheKing);
        }
    }
}
