namespace tic.Models
{
    public class GameMode
    {
        public List<List<char>> Board { get; set; }
        public char CurrentPlayer { get; set; }
        public bool IsGameOver { get; set; }

        public GameMode()
        {
            Board = new List<List<char>>();
            InitializeBoard();
            CurrentPlayer = 'X';
            IsGameOver = false;
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                List<char> row = new List<char>();
                for (int j = 0; j < 3; j++)
                {
                    row.Add(' ');
                }
                Board.Add(row);
            }
        }
    }
}
