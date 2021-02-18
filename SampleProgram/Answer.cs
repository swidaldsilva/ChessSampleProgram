using System;
using System.Collections.Generic;
using System.Linq;
using ChessLib;

namespace SampleProgram.ComplexGameNs
{
    #region enum

    /// <summary>
    /// enum for Pieces to be used in the game
    /// </summary>
    public enum Pieces
    {
        Knight,
        Bishop,
        Queen
    }

    public static class EnumExtensions
    {
        public static Enum GetRandomEnumValue(this Type t)
        {
            return Enum.GetValues(t)          // get values
                .OfType<Enum>()               // casts to Enum
                .OrderBy(e => Guid.NewGuid()) // shuffle order of results
                .FirstOrDefault();            // take first item
        }
    }

    #endregion

    #region Interface

    /// <summary>
    /// IPieceMoveinterface
    /// </summary>
    public interface IPieceMove
    {

        /// <summary>
        /// Returns the valid moves for a piece
        /// </summary>
        /// <param name="pos">current position</param>
        /// <returns>List of valid positions</returns>
        IEnumerable<Position> ValidMovesFor(Position pos);

    }

    #endregion

    #region abstract classes

    /// <summary>
    /// Piece factory method
    /// </summary>
    public abstract class PieceFactory
    {
        public abstract Piece GetPiece();

    }

    /// <summary>
    /// Piece creator
    /// </summary>
    public abstract class Piece
    {
        
        public abstract string PieceName { get; }
        public abstract int Moves { get; set; }
        public abstract Position StartPosition { get; set; }
        public abstract void Setup();
        public abstract Position Play(Dictionary<string, Position> currPosDict);

    }

    #endregion

    #region public methods

    public class KnightPiece : Piece
    {
        private readonly string _pieceName;
        private int _moves;
        private Position _startPosition;
        private readonly Random _rnd = new Random();
        public Position CurrentPosition { get; set; }

        public override string PieceName
        {
            get { return _pieceName; }
        }

        public override int Moves
        {
            get { return _moves; }
            set { _moves = value; }
        }

        public override Position StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }

        public KnightPiece(int moves, Position pos)
        {
            _pieceName = Pieces.Knight.ToString();
            _moves = moves;
            _startPosition = pos;
        }

        public override void Setup()
        {
            StartPosition = _startPosition;
        }

        public override Position Play(Dictionary<string, Position> currPosDict)
        {
            //Play the game moves 
            var knight = new KnightMove();
            var pos = _startPosition;
            Console.WriteLine("(Knight)0 My old position is {0}", pos);

            for (var move = 1; move <= _moves; move++)
            {
                var possibleMovesList = knight.ValidMovesFor(pos).ToList();
                possibleMovesList.RemoveAll(x => currPosDict.Any(y => y.Value.X == x.X && y.Value.Y == x.Y));

                var possibleMoves = possibleMovesList.ToArray();
                pos = possibleMoves[_rnd.Next(possibleMoves.Length)];
                CurrentPosition = pos;
                Console.WriteLine("(Knight){1}: My new position is {0}", pos, move);
            }

            return CurrentPosition;
        }


    }

    public class BishopPiece : Piece, IPieceMove
    {
        private readonly string _pieceName;
        private int _moves;
        private Position _startPosition;
        private readonly Random _rnd = new Random();
        public Position CurrentPosition { get; set; }

        public override string PieceName
        {
            get { return _pieceName; }
        }

        public override int Moves
        {
            get { return _moves; }
            set { _moves = value; }
        }

        public override Position StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }

        public BishopPiece(int moves, Position pos)
        {
            _pieceName = Pieces.Bishop.ToString();
            _moves = moves;
            _startPosition = pos;
        }

        public override void Setup()
        {
            StartPosition = _startPosition;
        }

        public override Position Play(Dictionary<string, Position> currPosDict)
        {
            //Play the game moves 
            var pos = _startPosition;
            Console.WriteLine("(Bishop)0: My old position is {0}", pos);

            for (var move = 1; move <= _moves; move++)
            {
                var possibleMovesList = ValidMovesFor(pos).ToList();
                possibleMovesList.RemoveAll(x => currPosDict.Any(y => y.Value.X == x.X && y.Value.Y == x.Y));

                var possibleMoves = possibleMovesList.ToArray();
                pos = possibleMoves[_rnd.Next(possibleMoves.Length)];
                CurrentPosition = pos;
                Console.WriteLine("(Bishop){1}: My new position is {0}", pos, move);
            }

            return CurrentPosition;
        }

        public IEnumerable<Position> ValidMovesFor(Position pos)
        {
            List<Position> result = new List<Position>();
            result.AddRange(PositionUtil.GetValidDiagonalPosition(pos));

            return result;
        }


    }

    public class QueenPiece : Piece, IPieceMove
    {
        private readonly string _pieceName;
        private int _moves;
        private Position _startPosition;
        private readonly Random _rnd = new Random();
        public Position CurrentPosition { get; set; }

        public override string PieceName
        {
            get { return _pieceName; }
        }

        public override int Moves
        {
            get { return _moves; }
            set { _moves = value; }
        }

        public override Position StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }

        public QueenPiece(int moves, Position pos)
        {
            _pieceName = Pieces.Queen.ToString();
            _moves = moves;
            _startPosition = pos;
        }

        public override void Setup()
        {
            StartPosition = _startPosition;
        }

        public override Position Play(Dictionary<string, Position> currPosDict)
        {
            //Play the game moves 
            var pos = _startPosition;
            Console.WriteLine("(Queen)0: My old position is {0}", pos);

            for (var move = 1; move <= _moves; move++)
            {
                var possibleMovesList = ValidMovesFor(pos).ToList();
                possibleMovesList.RemoveAll(x => currPosDict.Any(y => y.Value.X == x.X && y.Value.Y == x.Y));

                var possibleMoves = possibleMovesList.ToArray();
                pos = possibleMoves[_rnd.Next(possibleMoves.Length)];
                CurrentPosition = pos;
                Console.WriteLine("(Queen){1}: My new position is {0}", pos, move);
            }

            return CurrentPosition;
        }

        public IEnumerable<Position> ValidMovesFor(Position pos)
        {
            List<Position> result = new List<Position>();
            result.AddRange(PositionUtil.GetValidHorizontalPosition(pos));
            result.AddRange(PositionUtil.GetValidVerticalPosition(pos));
            result.AddRange(PositionUtil.GetValidDiagonalPosition(pos));

            return result;
        }


    }

    public class KnightFactory : PieceFactory
    {
        private int _moves;
        private Position _startPosition;

        public KnightFactory(int moves, Position pos)
        {
            _moves = moves;
            _startPosition = pos;
        }

        public override Piece GetPiece()
        {
            return new KnightPiece(_moves, _startPosition);
        }


    }

    public class BishopFactory : PieceFactory
    {
        private int _moves;
        private Position _startPosition;

        public BishopFactory(int moves, Position pos)
        {
            _moves = moves;
            _startPosition = pos;
        }

        public override Piece GetPiece()
        {
            return new BishopPiece(_moves, _startPosition);
        }


    }

    public class QueenFactory : PieceFactory
    {
        private int _moves;
        private Position _startPosition;

        public QueenFactory(int moves, Position pos)
        {
            _moves = moves;
            _startPosition = pos;
        }

        public override Piece GetPiece()
        {
            return new QueenPiece(_moves, _startPosition);
        }


    }

    public static class PositionUtil
    {
        private const int LowerLimit = 1;
        private const int UpperLimit = 8;

        /// <summary>
        /// Get valid horizontal position values for a piece
        /// </summary>
        /// <param name="pos">current position of the piece</param>
        /// <returns>new current position after piece moves </returns>
        public static IEnumerable<Position> GetValidHorizontalPosition(Position pos)
        {
            List<Position> result = new List<Position>();
            int x = pos.X;
            int y = pos.Y;

            for (int i = LowerLimit; i <= UpperLimit; i++)
            {
                if (i != y)
                {
                    Position newPos = new Position(x, i);
                    result.Add(newPos);
                }
            }


            return result;
        }

        /// <summary>
        /// Get valid vertical position values for a piece
        /// </summary>
        /// <param name="pos">current position of the piece</param>
        /// <returns>new current position after piece moves </returns>
        public static IEnumerable<Position> GetValidVerticalPosition(Position pos)
        {
            List<Position> result = new List<Position>();
            int x = pos.X;
            int y = pos.Y;

            for (int i = LowerLimit; i <= UpperLimit; i++)
            {
                if (i != x)
                {
                    Position newPos = new Position(i, y);
                    result.Add(newPos);
                }
            }


            return result;
        }

        /// <summary>
        /// Method to get valid diagonal values for a peice
        /// </summary>
        /// <param name="pos">Current position of piece</param>
        /// <returns>Newcurrent position after peice moves</returns>
        public static IEnumerable<Position> GetValidDiagonalPosition(Position pos)
        {
            List<Position> result = new List<Position>();
            int x = pos.X;
            int y = pos.Y;


            //Direction 1 = x+1, y+1
            while ((LowerLimit <= x && x < UpperLimit) && (LowerLimit <= y && y < UpperLimit))
            {
                x = x + 1;
                y = y + 1;
                Position newPos = new Position(x, y);
                result.Add(newPos);
            }
            //Direction 1 = x-1, y-1
            //Set back x and y to original values
            x = pos.X;
            y = pos.Y;
            while ((LowerLimit < x && x <= UpperLimit) && (LowerLimit < y && y <= UpperLimit))
            {
                x = x - 1;
                y = y - 1;
                Position newPos = new Position(x, y);
                result.Add(newPos);
            }
            //Direction 1 = x+1, y-1
            //Set back x and y to original values
            x = pos.X;
            y = pos.Y;
            while ((LowerLimit <= x && x < UpperLimit) && (LowerLimit < y && y <= UpperLimit))
            {
                x = x + 1;
                y = y - 1;
                Position newPos = new Position(x, y);
                result.Add(newPos);
            }
            //Direction 1 = x-1, y+1
            //Set back x and y to original values
            x = pos.X;
            y = pos.Y;
            while ((LowerLimit < x && x <= UpperLimit) && (LowerLimit <= y && y < UpperLimit))
            {
                x = x - 1;
                y = y + 1;
                Position newPos = new Position(x, y);
                result.Add(newPos);
            }

            // make sure remove the current position
            result.Remove(pos);


            return result;
        }
    }

    public class ComplexGame
    {
        private readonly Random _rnd = new Random();
        public Dictionary<string, Position> CurrPositionDict = new Dictionary<string, Position>();

        ///// <summary>
        ///// Call the method using reflection
        ///// </summary>
        ///// <param name="piece">the piece<param>
        ///// <param name="methodName">Method to be called on piece object</param>
        ///// <param name="param">parameters to be passed to the method being called</param>
        //private static void CreatePieceTypeAndCallMethod(string piece, string methodName, object[] param)
        //{
        //    Type type = Type.GetType("SampleProgram.ComplexGameNs." + piece);
        //    var pieceType = (IPiece)Activator.CreateInstance(type);
        //    var parameterTypes = new Type[] {typeof(int) };

        //    var method = param == null ? type.GetMethod(methodName) : type.GetMethod(methodName, parameterTypes);
        //    method.Invoke(pieceType, param);
        //}
        public void Setup()
        {
            //Set start position of all pieces 
            foreach (Pieces piece in Enum.GetValues(typeof(Pieces)))
            {
                var factory = GetFactory(piece, 0, CurrPositionDict);
                var pieceObj = factory.GetPiece(); // return the piece object 
                pieceObj.Setup(); // calls the Setup method of the piece object
                CurrPositionDict.Add(pieceObj.PieceName, pieceObj.StartPosition);
            }

        }

        public void Play(int moves)
        {
            for (var move = 1; move <= moves; move++)
            {
                // Get random piece for each move
                var piece = typeof(Pieces).GetRandomEnumValue();
                var factory = GetFactory((Pieces)piece, 1, CurrPositionDict); // set move = 1 as after each move we have to select other piece on random basis
                var pieceObj = factory.GetPiece(); // return the piece object for the randomly selected piece
                var currPosition = pieceObj.Play(CurrPositionDict); // calls the Play method of the piece object

                //Set the current Position for the piece
                CurrPositionDict[pieceObj.PieceName] = currPosition;
            }

        }

        private PieceFactory GetFactory(Pieces piece, int moves, Dictionary<string, Position> currPosDict)
        {
            PieceFactory factory = null;
            switch (piece)
            {
                case Pieces.Knight:
                    var newPositionK = currPosDict.ContainsKey(Pieces.Knight.ToString()) ? currPosDict[Pieces.Knight.ToString()] : new Position(3, 3);
                    factory = new KnightFactory(moves, newPositionK);
                    break;
                case Pieces.Bishop:
                    var newPositionB = currPosDict.ContainsKey(Pieces.Bishop.ToString()) ? currPosDict[Pieces.Bishop.ToString()] : new Position(2, 2);
                    factory = new BishopFactory(moves, newPositionB);
                    break;
                case Pieces.Queen:
                    var newPositionQ = currPosDict.ContainsKey(Pieces.Queen.ToString()) ? currPosDict[Pieces.Queen.ToString()] : new Position(1, 1);
                    factory = new QueenFactory(moves, newPositionQ);
                    break;
                default:
                    break;

            }

            return factory;
        }


    }

    #endregion






}
