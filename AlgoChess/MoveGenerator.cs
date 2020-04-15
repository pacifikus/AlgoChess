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
		private Dictionary<FigureType, List<int>> _captureShifts;

		public MoveGenerator(Board board)
		{
			_board = board;
			_moves = new List<Move>();
			InitShifts();
		}

		private void InitShifts()
		{
			_captureShifts = new Dictionary<FigureType, List<int>>();
			_captureShifts.Add(FigureType.Pawn, new List<int>() { 1, -1 });
			_captureShifts.Add(FigureType.Bishop, new List<int>() { 15, 17, -15, -17 });
			_captureShifts.Add(FigureType.Rook, new List<int>() { 1, -1, 16, -16 });
			_captureShifts.Add(FigureType.Knight, new List<int>() { -18, -14, 18, 14, -33, -31,33,31 }); 
			_captureShifts.Add(FigureType.Queen, new List<int>() { 15, 17, -15, -17, 1, -1, 16, -16 });
			_captureShifts.Add(FigureType.King, new List<int>() { 15, 17, -15, -17, 1, -1, 16, -16 });
		}

		private void GeneratePawnMoves(Figure figure, int pawnShift)
		{
			for (int i = 1; i < 3; i++)
			{
				int nextPosition = figure.Position256 + pawnShift * i;
				if (IsWithinBoard(nextPosition) && IsFreeField(nextPosition))
				{
					_moves.Add(new Move(figure.Position256, nextPosition));
				}
			}

			// Captures.
			Color enemyColor = GetEnemyColor(figure.Color);
			var shifts = _captureShifts[FigureType.Pawn];

			for (int i = 0; i < shifts.Count; i++)
			{
				int nextPosition = figure.Position256 + pawnShift + shifts[i];
				if (IsWithinBoard(nextPosition) && IsEnemysField(nextPosition, enemyColor))
				{
					_moves.Add(new Move(figure.Position256, nextPosition));
				}
			}
		}

		private void GenerateMoves(Figure figure)
		{
			Color enemyColor = GetEnemyColor(figure.Color);
			var shifts = _captureShifts[figure.Type];

			for (int i = 0; i < shifts.Count; i++)
			{
				int nextPosition = figure.Position256;

				while (true)
				{
					nextPosition += shifts[i];

					if (IsWithinBoard(nextPosition))
					{
						if (IsFreeField(nextPosition))
						{
							_moves.Add(new Move(figure.Position256, nextPosition));
						}
						else break; 

						if (IsEnemysField(nextPosition, enemyColor))
						{
							_moves.Add(new Move(figure.Position256, nextPosition));
							break;
						}
					}
					else break;

					if( figure.Type == FigureType.King || figure.Type == FigureType.Knight)
					{
						break;
					}
				}
			}
		}

		private bool IsEnemysField(int nextIndex, Color enemyColor)
		{
			return _board.Fields[nextIndex] != -1 && _board.Figures[_board.Fields[nextIndex]].Color == enemyColor;
		}

		private bool IsFreeField(int nextIndex)
		{
			return _board.Fields[nextIndex] == -1;
		}

		private bool IsWithinBoard(int nextIndex)
		{
			return _board.Fields[nextIndex] != int.MinValue;
		}

		private Color GetEnemyColor(Color hostColor)
		{
			return hostColor == Color.White ? Color.Black : Color.White;
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
						// TODO: en passant 
						var shift = figures[i].Color == Color.White ? -16 : 16;
						GeneratePawnMoves(figures[i], shift);
						break;
					default:
						GenerateMoves(figures[i]);
						break;
				}
			}

			return _moves;
		}
	}
}
