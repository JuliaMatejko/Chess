using System.Collections.Generic;

namespace Chess.Model
{
    class Board : List<List<Field>>
    {
        public const int BoardSize = 8;
        public static string[] Files { get; } = new string[BoardSize] { "a", "b", "c", "d", "e", "f", "g", "h" };
        public static string[] Ranks { get; } = new string[BoardSize] { "1", "2", "3", "4", "5", "6", "7", "8" };
        public static string[] Positions { get; } = CreatePositionNames();

        public static Board CreateABoard()
        {
            Board board = new Board();

            for (var i = 0; i < BoardSize; i++)
            {
                board.Add(new List<Field>());

                for (var j = 0; j < BoardSize; j++)
                {
                    board[i].Add(new Field(Files[i], Ranks[j], null));
                }
            }
            return board;
        }

        private static string[] CreatePositionNames()
        {
            string[] fieldNames = new string[BoardSize * BoardSize];
            int count = 0;
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    fieldNames[count] = Files[i] + Ranks[j];
                    count++;
                }
            }
            return fieldNames;
        }
    }
}
