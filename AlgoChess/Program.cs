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
			var game = new Game();
			Console.WriteLine(game.ToASCII());
			Console.ReadLine();
		}
	}
}
