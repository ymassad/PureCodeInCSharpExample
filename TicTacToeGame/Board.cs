using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TicTacToeGame
{
    [IsPure]
    public sealed class Board
    {
        public Board()
        {
            Cells = ImmutableArray.Create(
                ImmutableArray.Create(CellStatus.Empty, CellStatus.Empty, CellStatus.Empty),
                ImmutableArray.Create(CellStatus.Empty, CellStatus.Empty, CellStatus.Empty),
                ImmutableArray.Create(CellStatus.Empty, CellStatus.Empty, CellStatus.Empty));
        }
        public Board(ImmutableArray<ImmutableArray<CellStatus>> cells)
        {
            Cells = cells;
        }

        private ImmutableArray<ImmutableArray<CellStatus>> Cells { get; }

        public static bool AnyLineIsFullOf(Board board, CellStatus status)
        {
            return lines.Any(
                 line => line.All(
                     cell => board.Cells[cell.row][cell.column] == status));
        }

        public static Player? GetWinner(Board board)
        {
            if (AnyLineIsFullOf(board, CellStatus.HasX))
                return Player.X;
            else if (AnyLineIsFullOf(board, CellStatus.HasO))
                return Player.O;

            return null;
        }

        public CellStatus GetCell(int row, int column)
        {
            return Cells[row][column];
        }

        public static Board SetCell(Board board, int row, int column, CellStatus newValue)
        {
            return new Board(board.Cells.SetItem(row, board.Cells[row].SetItem(column, newValue)));
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


        private readonly static ImmutableArray<ImmutableArray<(int row, int column)>> lines =
            ImmutableArray.Create(
                ImmutableArray.Create((0, 0), (0, 1), (0, 2)),
                ImmutableArray.Create((1, 0), (1, 1), (1, 2)),
                ImmutableArray.Create((2, 0), (2, 1), (2, 2)),
                ImmutableArray.Create((0, 0), (1, 0), (2, 0)),
                ImmutableArray.Create((0, 1), (1, 1), (2, 1)),
                ImmutableArray.Create((0, 2), (1, 2), (2, 2)),
                ImmutableArray.Create((0, 0), (1, 1), (2, 2)),
                ImmutableArray.Create((0, 2), (1, 1), (2, 0)));

    }
}