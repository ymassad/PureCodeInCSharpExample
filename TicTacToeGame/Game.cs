using System;

namespace TicTacToeGame
{
    [IsPure]
    public sealed class Game
    {
        private readonly Action<string> writeToConsole;
        private readonly Func<string> readFromConsole;

        public Game(Action<string> writeToConsole, Func<string> readFromConsole)
        {
            this.writeToConsole = writeToConsole;
            this.readFromConsole = readFromConsole;
        }

        public void PlayMultipleTimes()
        {
            int numberOfTimesXWon = 0;
            int numberOfTimesOWon = 0;

            do
            {
                var winner = PlayGame();

                if (winner == Player.O)
                    numberOfTimesOWon++;
                else if(winner == Player.X)
                    numberOfTimesXWon++;

                writeToConsole("Number of times X won: " + numberOfTimesXWon);
                writeToConsole("Number of times O won: " + numberOfTimesOWon);

            } while (PlayAgain());
        }

        private bool PlayAgain()
        {

            bool IsYes(string input) => input.Equals("yes");
            bool IsNo(string input) => input.Equals("no");

            string line;
            do
            {
                writeToConsole("Play again? yes/no");

                line = readFromConsole();
            } while (!IsYes(line) && !IsNo(line));

            return IsYes(line);
        }

        private Player? PlayGame()
        {
            Board board = new Board();

            Player currentPlayer = Player.X;

            void SwichPlayer()
            {
                currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
            }

            Player? winner = null;

            while (!winner.HasValue && !board.IsFull())
            {
                writeToConsole("It's player " + currentPlayer + "'s turn");

                board = PlayOneTurn(currentPlayer, board);

                winner = Board.GetWinner(board);

                SwichPlayer();
            }

            if (winner.HasValue)
            {
                writeToConsole(winner + " is a winner");
            }
            else
            {
                writeToConsole("Game over. No winner");
            }

            return winner;
        }

        private Board PlayOneTurn(Player currentPlayer, Board board)
        {
            while (true)
            {
                var (row, column) = ReadRowAndColumnFromConsole();

                var result = PlayBoard(currentPlayer, board, row, column);

                if (!result.success)
                {
                    writeToConsole("Cell is not empty");
                }
                else
                {
                    return result.updatedBoard;
                }
            }
        }

        private (int row, int column) ReadRowAndColumnFromConsole()
        {
            writeToConsole("Please specify row (1-3):");

            int row;
            while (!PureInt.TryParseCultureInvariant(readFromConsole(), out row) || row < 1 || row > 3)
                writeToConsole("Invalid value");

            writeToConsole("Please specify column (1-3):");

            int column;
            while (!PureInt.TryParseCultureInvariant(readFromConsole(), out column) || column < 1 || column > 3)
                writeToConsole("Invalid value");

            return (row - 1, column - 1);
        }

        public (bool success, Board updatedBoard) PlayBoard(Player currentPlayer, Board board, int row, int column)
        {
            var cell = board.GetCell(row, column);

            if (cell == CellStatus.HasO || cell == CellStatus.HasX)
                return (false, null);

            Board newBoard;

            if (currentPlayer == Player.O)
                newBoard = Board.SetCell(board, row, column, CellStatus.HasO);
            else
                newBoard = Board.SetCell(board, row, column, CellStatus.HasX);

            newBoard.PrintToConsole(writeToConsole);

            return (true, newBoard);
        }
    }
}