using System;
using System.Collections.Generic;
using System.Linq;
using ChessLib;
using NUnit.Framework;
using SampleProgram.ComplexGameNs;

namespace SampleProgram.Test
{
    [TestFixture]
    public class TestFixture1
    {
        [Test]
        public void TestKnightMoveFromInsideBoard()
        {
            var pos = new Position(3, 3);
            var knight = new KnightMove();

            var moves = knight.ValidMovesFor(pos).ToArray();

            Assert.IsNotNull(moves);
            Assert.AreEqual(8, moves.Length);

            foreach (var move in moves)
            {
                switch (Math.Abs(move.X - pos.X))
                {
                    case 1:
                        Assert.AreEqual(2, Math.Abs(move.Y - pos.Y));
                        break;
                    case 2:
                        Assert.AreEqual(1, Math.Abs(move.Y - pos.Y));
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
            }
        }

        [Test]
        public void TestKnightMoveFromCorner()
        {
            var pos = new Position(1, 1);
            var knight = new KnightMove();

            var moves = new HashSet<Position>(knight.ValidMovesFor(pos));

            Assert.IsNotNull(moves);
            Assert.AreEqual(2, moves.Count);

            var possibles = new[] {new Position(2, 3), new Position(3, 2)};

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [Test]
        public void TestPosition()
        {
            var pos = new Position(1, 1);
            Assert.AreEqual(1, pos.X);
            Assert.AreEqual(1, pos.Y);

            var pos2 = new Position(1, 1);

            Assert.AreEqual(pos, pos2);
        }



        [Test]
        public void TestBishopMoveFromCorner()
        {
            var pos = new Position(1, 1);
            var bishop = new BishopPiece(1, pos);

            var moves = new HashSet<Position>(bishop.ValidMovesFor(pos));

            Assert.IsNotNull(moves);
            Assert.AreEqual(7, moves.Count);

            var possibles = new[] { new Position(2, 2), new Position(3, 3), new Position(4, 4), new Position(5, 5), new Position(6, 6), new Position(7, 7), new Position(8, 8) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [Test]
        public void TestBishopMoveFromInsideTheBoard()
        {
            var pos = new Position(2, 5);
            var bishop = new BishopPiece(1, pos);

            var moves = new HashSet<Position>(bishop.ValidMovesFor(pos));

            Assert.IsNotNull(moves);
            Assert.AreEqual(9, moves.Count);

            var possibles = new[] { new Position(1, 4), new Position(3, 6), new Position(4, 7), new Position(5, 8), new Position(1, 6), new Position(3, 4), new Position(4, 3), new Position(5, 2), new Position(6, 1) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }


        [Test]
        public void TestQueenpMoveFromCorner()
        {
            var pos = new Position(8, 8);
            var queen = new QueenPiece(1, pos);

            var moves = new HashSet<Position>(queen.ValidMovesFor(pos));

            Assert.IsNotNull(moves);
            Assert.AreEqual(21, moves.Count);

            var possibles = new[] { new Position(7, 8), new Position(6, 8), new Position(5, 8), new Position(4, 8), new Position(3, 8), new Position(2, 8), new Position(1, 8),
                new Position(8, 7), new Position(8, 6), new Position(8, 5), new Position(8, 4), new Position(8, 3), new Position(8, 2), new Position(8, 1),
                new Position(7, 7), new Position(6, 6), new Position(5, 5), new Position(4, 4), new Position(3, 3), new Position(2, 2), new Position(1, 1) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [Test]
        public void TestQueenpMoveFromInsideTheBoard()
        {
            var pos = new Position(2, 3);
            var queen = new QueenPiece(1, pos);

            var moves = new HashSet<Position>(queen.ValidMovesFor(pos));

            Assert.IsNotNull(moves);
            Assert.AreEqual(23, moves.Count);

            var possibles = new[] { new Position(7, 3), new Position(6, 3), new Position(5, 3), new Position(4, 3), new Position(3, 3), new Position(8, 3), new Position(1, 3),
                new Position(2, 7), new Position(2, 6), new Position(2, 5), new Position(2, 4), new Position(2, 8), new Position(2, 2), new Position(2, 1),
                new Position(1, 2), new Position(1, 4), new Position(3, 4), new Position(4, 5), new Position(5, 6), new Position(6, 7), new Position(7, 8), new Position(3, 2), new Position(4, 1) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }
        }

        [Test]
        public void TestComplexGameSetupMethod()
        {
            var game = new ComplexGame();
            game.Setup();

            Assert.IsNotNull(game.CurrPositionDict);
            Assert.AreEqual(3, game.CurrPositionDict.Count);

        }

        [Test]
        public void TestComplexGamePlayMethod()
        {
            var game = new ComplexGame();
            game.Play(1);

            Assert.AreEqual(game.CurrPositionDict.Count, 1);

        }

        [Test]
        public void TestQueenSetupMethod()
        {
            Position pos = new Position(1, 1);
            var queen = new QueenPiece(3, pos);
            queen.Setup();

            Assert.AreEqual(queen.StartPosition, pos);

        }

        [Test]
        public void TestQueenPlayMethod()
        {
            Position pos = new Position(1, 1);
            Dictionary<string, Position> posDict = new Dictionary<string, Position>();
            var queen = new QueenPiece(3, pos);
            var currPos = queen.Play(posDict);

            Assert.AreEqual(queen.StartPosition, pos);
            Assert.AreNotEqual(queen.StartPosition, currPos);

        }

        // TODO :  add tests for BishopPiece and KnightPiece play and setup methods

        [Test]
        public void TestGetValidHorizontalPositionMethod()
        {
            Position pos = new Position(1, 1);
            var moves = PositionUtil.GetValidHorizontalPosition(pos).ToList();

            Assert.IsNotNull(moves);
            Assert.AreEqual(7, moves.Count);

            var possibles = new[] { new Position(1, 2), new Position(1, 3), new Position(1, 4), new Position(1, 5), new Position(1, 6), new Position(1, 7), new Position(1, 8) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }

        }

        [Test]
        public void TestGetValidVerticalPositionMethod()
        {
            Position pos = new Position(1, 1);
            var moves = PositionUtil.GetValidVerticalPosition(pos).ToList();

            Assert.IsNotNull(moves);
            Assert.AreEqual(7, moves.Count);

            var possibles = new[] { new Position(2, 1), new Position(3, 1), new Position(4, 1), new Position(5, 1), new Position(6, 1), new Position(7, 1), new Position(8, 1) };

            foreach (var possible in possibles)
            {
                Assert.IsTrue(moves.Contains(possible));
            }

        }

        // TODO:  add tests for GetValidDiagonalPosition method


    }
}
