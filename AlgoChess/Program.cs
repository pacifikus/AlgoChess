using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	class Program
	{
		static void Main(string[] args)
		{
			var fen = "rnbq1bnr/pppkpppp/8/3p4/4P3/5N2/PPPP1PPP/RNBQKB1R w KQ - 2 3";
			var game = new Game(fen);
			Console.WriteLine(game.ToASCII());
			Console.ReadLine();
		}
	}
}
