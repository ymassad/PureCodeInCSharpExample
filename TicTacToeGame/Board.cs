using System;
using System.Linq;

namespace TicTacToeGame
{
    public sealed class Board
    {
        public Board()
        {
            Cells = new[] { new CellStatus[3], new CellStatus[3], new CellStatus[3] };
        }

        private CellStatus[][] Cells { get; }

        public Player? Winner { get; private set; }

        public CellStatus GetCell(int row, int column)
        {
            return Cells[row][column];
        }

        public void SetCell(int row, int column, CellStatus newValue)
        {
            Cells[row][column] = newValue;

            if (newValue != CellStatus.Empty)
            {
                if (lines.Any(line => line.All(cell => Cells[cell.row][cell.column] == newValue)))
                {
                    Winner = newValue == CellStatus.HasO ? Player.O : Player.X;

                    if (Winner == Player.O)
                        Game.numberOfTimesOWon++;
                    else
                        Game.numberOfTimesXWon++;
                }

            }

        }

        public bool IsFull() => Cells.All(row => row.All(x => x != CellStatus.Empty));

        public void PrintToConsole(Action<string> writeToConsole)
        {
            writeToConsole(FormatCell(Cells[0][0]) + " " + FormatCell(Cells[0][1]) + " " + FormatCell(Cells[0][2]));
            writeToConsole(FormatCell(Cells[1][0]) + " " + FormatCell(Cells[1][1]) + " " + FormatCell(Cells[1][2]));
            writeToConsole(FormatCell(Cells[2][0]) + " " + FormatCell(Cells[2][1]) + " " + FormatCell(Cells[2][2]));
        }

        public string FormatCell(CellStatus status)
        {
            if (status == CellStatus.Empty)
                return "_";
            if (status == CellStatus.HasX)
                return "X";

            return "O";
        }

        private static (int row, int column)[][] lines =
        {
            new []{(0,0), (0,1), (0,2)},
            new []{(1,0), (1,1), (1,2)},
            new []{(2,0), (2,1), (2,2)},
            new []{(0,0), (1,0), (2,0)},
            new []{(0,1), (1,1), (2,1)},
            new []{(0,2), (1,2), (2,2)},
            new []{(0,0), (1,1), (2,2)},
            new []{(0,2), (1,1), (2,0)}
        };

    }
}