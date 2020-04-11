using AlgoChess.Entities;
using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	public class MoveGenerator
	{
		private Board _board;
		private List<Move> _moves;

		public MoveGenerator(Board board)
		{
			_board = board;
			_moves = new List<Move>();
		}

		private void GeneratePawnFieldMove(Figure figure, int shift)
		{
			// TODO: create constants for all figures shifts
			int nextPosition = figure.Position + shift;
			if (IsWithinBoard(nextPosition) && IsFieldFree(nextPosition))
			{
				_moves.Add(new Move(figure.Position, nextPosition));
			}
			// captures
		}

		private bool IsFieldFree(int nextIndex)
		{
			return _board.Fields[nextIndex] == 0;
		}

		private static bool IsWithinBoard(int nextIndex)
		{
			// TODO: fill all constraints
			return nextIndex >= 0 && nextIndex < 64;
		}

		public List<Move> GenerateMoves(Color color)
		{
			var figures = _board.GetFiguresByColor(color);

			// TODO: first - captures
			// TODO: mvv/lva

			for (int i = 0; i < figures.Count; i++)
			{
				switch (figures[i].Type)
				{
					case FigureType.Pawn:
						int shift = color == Color.White ? -8 : 8;
						GeneratePawnFieldMove(figures[i], shift);
						GeneratePawnFieldMove(figures[i], shift * 2);
						break;
					default:
						break;
				}
			}

			return _moves;
		}
	}
}
