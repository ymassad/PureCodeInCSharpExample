using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game(Console.WriteLine).PlayMultipleTimes();
        }
    }
}
