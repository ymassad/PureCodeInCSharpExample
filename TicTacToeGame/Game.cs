using System;

namespace TicTacToeGame
{
    public sealed class Game
    {
        public static int numberOfTimesXWon;
        public static int numberOfTimesOWon;

        public void PlayMultipleTimes()
        {
            do
            {
                PlayGame();

                Console.WriteLine("Number of times X won: " + numberOfTimesXWon);
                Console.WriteLine("Number of times O won: " + numberOfTimesOWon);
            } while (PlayAgain());
        }

        private bool PlayAgain()
        {

            bool IsYes(string input) => input.Equals("yes");
            bool IsNo(string input) => input.Equals("no");

            string line;
            do
            {
                Console.WriteLine("Play again? yes/no");

                line = Console.ReadLine();
            } while (!IsYes(line) && !IsNo(line));

            return IsYes(line);
        }

        private void PlayGame()
        {
            Board board = new Board();

            Player currentPlayer = Player.X;

            void SwichPlayer()
            {
                currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
            }

            while (!board.Winner.HasValue && !board.IsFull())
            {
                Console.WriteLine("It's player " + currentPlayer + "'s turn");

                PlayOneTurn(currentPlayer, board); 

                SwichPlayer();
            }

            if (board.Winner.HasValue)
            {
                Console.WriteLine(board.Winner + " is a winner");
            }
            else
            {
                Console.WriteLine("Game over. No winner");
            }
        }

        private void PlayOneTurn(Player currentPlayer, Board board)
        {
            while (true)
            {
                var (row, column) = ReadRowAndColumnFromConsole();

                if (!PlayBoard(currentPlayer, board, row, column))
                {
                    Console.WriteLine("Cell is not empty");
                }
                else
                {
                    break;
                }
            }
        }

        private (int row, int column) ReadRowAndColumnFromConsole()
        {
            Console.WriteLine("Please specify row (1-3):");

            int row;
            while (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 3)
                Console.WriteLine("Invalid value");

            Console.WriteLine("Please specify column (1-3):");

            int column;
            while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 3)
                Console.WriteLine("Invalid value");

            return (row - 1, column - 1);
        }

        public bool PlayBoard(Player currentPlayer, Board board, int row, int column)
        {
            var cell = board.GetCell(row, column);

            if (cell == CellStatus.HasO || cell == CellStatus.HasX)
                return false;

            if (currentPlayer == Player.O)
                board.SetCell(row, column, CellStatus.HasO);
            else 
                board.SetCell(row, column, CellStatus.HasX);

            board.PrintToConsole();

            return true;
        }
    }
}