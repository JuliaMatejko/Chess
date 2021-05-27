using Chess.Model;
namespace Chess
{
    class Program
    {
        public static Game Game { get; set; } = new Game();

        static void Main(string[] args)
        {
            Game.StartGame();
        }
    }
}
