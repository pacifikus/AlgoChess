using AlgoChess.Entities;
using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	public class PositionEvaluator
	{
		private Board _board;

		// all for black figures

		private int[] _pawnCoeffs = new int[64]
		{   0,  0,  0,  0,  0,  0,  0,  0,
			50, 50, 50, 50, 50, 50, 50, 50,
			10, 10, 20, 30, 30, 20, 10, 10,
			 5,  5, 10, 25, 25, 10,  5,  5,
			 0,  0,  0, 20, 20,  0,  0,  0,
			 5, -5,-10,  0,  0,-10, -5,  5,
			 5, 10, 10,-20,-20, 10, 10,  5,
			 0,  0,  0,  0,  0,  0,  0,  0
		};

		private int[] _knightCoeffs = new int[64]
		{
			-50,-40,-30,-30,-30,-30,-40,-50,
			-40,-20,  0,  0,  0,  0,-20,-40,
			-30,  0, 10, 15, 15, 10,  0,-30,
			-30,  5, 15, 20, 20, 15,  5,-30,
			-30,  0, 15, 20, 20, 15,  0,-30,
			-30,  5, 10, 15, 15, 10,  5,-30,
			-40,-20,  0,  5,  5,  0,-20,-40,
			-50,-40,-30,-30,-30,-30,-40,-50
		};

		private int[] _bishopCoeffs = new int[64]
		{
			-20,-10,-10,-10,-10,-10,-10,-20,
			-10,  0,  0,  0,  0,  0,  0,-10,
			-10,  0,  5, 10, 10,  5,  0,-10,
			-10,  5,  5, 10, 10,  5,  5,-10,
			-10,  0, 10, 10, 10, 10,  0,-10,
			-10, 10, 10, 10, 10, 10, 10,-10,
			-10,  5,  0,  0,  0,  0,  5,-10,
			-20,-10,-10,-10,-10,-10,-10,-20,
		};

		private int[] _rookCoeffs = new int[64]
		{
			 0,  0,  0,  0,  0,  0,  0,  0,
			 5, 10, 10, 10, 10, 10, 10,  5,
			 -5,  0,  0,  0,  0,  0,  0, -5,
			 -5,  0,  0,  0,  0,  0,  0, -5,
			 -5,  0,  0,  0,  0,  0,  0, -5,
			 -5,  0,  0,  0,  0,  0,  0, -5,
			 -5,  0,  0,  0,  0,  0,  0, -5,
			  0,  0,  0,  5,  5,  0,  0,  0
		};

		private int[] _queenCoeffs = new int[64]
		{
			-20,-10,-10, -5, -5,-10,-10,-20,
			-10,  0,  0,  0,  0,  0,  0,-10,
			-10,  0,  5,  5,  5,  5,  0,-10,
			 -5,  0,  5,  5,  5,  5,  0, -5,
			  0,  0,  5,  5,  5,  5,  0, -5,
			-10,  5,  5,  5,  5,  5,  0,-10,
			-10,  0,  5,  0,  0,  0,  0,-10,
			-20,-10,-10, -5, -5,-10,-10,-20
		};

		private int[] _kingMiddleCoeffs = new int[64]
		{
			-30,-40,-40,-50,-50,-40,-40,-30,
			-30,-40,-40,-50,-50,-40,-40,-30,
			-30,-40,-40,-50,-50,-40,-40,-30,
			-30,-40,-40,-50,-50,-40,-40,-30,
			-20,-30,-30,-40,-40,-30,-30,-20,
			-10,-20,-20,-20,-20,-20,-20,-10,
			 20, 20,  0,  0,  0,  0, 20, 20,
			 20, 30, 10,  0,  0, 10, 30, 20
		};

		private int[] _kingEndCoeffs = new int[64]
		{
			-50,-40,-30,-20,-20,-30,-40,-50,
			-30,-20,-10,  0,  0,-10,-20,-30,
			-30,-10, 20, 30, 30, 20,-10,-30,
			-30,-10, 30, 40, 40, 30,-10,-30,
			-30,-10, 30, 40, 40, 30,-10,-30,
			-30,-10, 20, 30, 30, 20,-10,-30,
			-30,-30,  0,  0,  0,  0,-30,-30,
			-50,-30,-30,-30,-30,-30,-30,-50
		};

		private Dictionary<FigureType, int> _scores;

		public PositionEvaluator(Board currentBoard)
		{
			_board = currentBoard;
			InitScores();
			ComputeGamePhaseIndex();
		}

		// TODO: change scores in the end.
		// TODO: special pairs
		private void InitScores()
		{
			_scores = new Dictionary<FigureType, int>();
			_scores.Add(FigureType.Pawn, 100);
			_scores.Add(FigureType.Bishop, 300);
			_scores.Add(FigureType.Knight, 300);
			_scores.Add(FigureType.Rook, 500);
			_scores.Add(FigureType.Queen, 900);
			_scores.Add(FigureType.King, 100000);
		}

		public int Evaluate(Color turn)
		{
			// TODO: kings castling
			// TODO: kings safety
			// TODO: doubled, isolated, backward pawns
			// TODO: count attacked fields
			// TODO: x-ray attack

			List<Figure> whiteFigures = _board.GetFiguresByColor(Color.White);
			List<Figure> blackFigures = _board.GetFiguresByColor(Color.Black);

			var whiteMaterial = CountMaterial(whiteFigures);
			var blackMaterial = CountMaterial(blackFigures);
			var gamePhase = ComputeGamePhaseIndex();
			var whitePosition = CountPositionScore(whiteFigures);
			var blackPosition = CountPositionScore(whiteFigures);

			var score = whiteMaterial - blackMaterial + whitePosition - blackPosition;

			return turn == Color.White ? score : -score;
		}

		private int CountPositionScore(List<Figure> figures)
		{
			int score = 0;
			for (int i = 0; i < figures.Count; i++)
			{
				FigureType type = figures[i].Type;
				int position = figures[i].Position64;
				switch (type)
				{
					case FigureType.Rook:
						score += _rookCoeffs[position];
						break;
					case FigureType.Knight:
						score += _knightCoeffs[position];
						break;
					case FigureType.Bishop:
						score += _bishopCoeffs[position];
						break;
					case FigureType.King:
						score += _kingMiddleCoeffs[position];
						// TODO: middle/end game change 
						//score += _kingEndCoeffs[position];
						break;
					case FigureType.Queen:
						score += _queenCoeffs[position];
						break;
					case FigureType.Pawn:
						score += +_pawnCoeffs[position];
						break;
					default:
						break;
				}
			}
			return score;
		}

		private double ComputeGamePhaseIndex()
		{
			double maxPhaseIndex = 32.0;
			double currentPhaseIndex = 0.0;

			var phaseWeights = new Dictionary<FigureType, int>
			{
				[FigureType.Queen] = 6,
				[FigureType.Rook] = 3,
				[FigureType.Bishop] = 1,
				[FigureType.Knight] = 1,
				[FigureType.Pawn] = 0,
				[FigureType.King] = 0
			};

			for (int i = 0; i < _board.Figures.Count; i++)
			{
				FigureType type = _board.Figures[i].Type;
				currentPhaseIndex += phaseWeights[type];
			}
			return currentPhaseIndex / maxPhaseIndex;
		}

		private int CountMaterial(List<Figure> figures)
		{
			int score = 0;
			for (int i = 0; i < figures.Count; i++)
			{
				FigureType type = figures[i].Type;
				score += _scores[type];
			}
			return score;
		}
	}
}
