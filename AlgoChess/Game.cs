using AlgoChess.Entities;
using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	public class Game
	{
		private const string StartPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
		private const int MaxPly = 6;
		private Color _turn;
		private int _moveNumber;
		private Board _currentBoard;

		public Game()
		{
			CurrentPosition = StartPosition;
			InitGame();
		}

		public Game(string fen)
		{
			if (IsValidFEN(fen))
			{
				CurrentPosition = fen;
			}
			else
			{
				CurrentPosition = StartPosition;
			}
			InitGame();
		}

		public string CurrentPosition { get; set; }

		public void LoadFromFEN(string fen)
		{
			// TODO: currentPosition = parse(fen)
		}

		public void ResetBoard()
		{
			// TODO: currentPosition = START_POSITION, reset all
		}

		public string ToASCII()
		{
			return _currentBoard.ToASCII();
		}

		public void Undo()
		{
			// TODO: Undo last move.
		}

		private void InitGame()
		{
			string[] fenSections = CurrentPosition.Split(' ');
			_currentBoard = new Board(fenSections[0]);
			_turn = (Color)fenSections[1][0];
			// TODO: parse castling
			// TODO: parse en passant
			// TODO: parse halfmove clock
			_moveNumber = int.Parse(fenSections[5]);
		}

		private bool IsCheckmate()
		{
			// TODO: check position for the checkmate.
			return false;
		}

		private bool IsDraw()
		{
			// TODO: check position for the draw.
			return false;
		}

		private bool IsMaterialInsufficient()
		{
			// TODO: check for the material insufficient .
			return false;
		}

		private bool IsValidFEN(string fen)
		{
			// TODO: validate fen
			return true;
		}

		public List<Move> GetAvailableMoves()
		{
			// TODO: sort moves
			var generator = new MoveGenerator(_currentBoard);
			return generator.GenerateMoves(_turn);
		}

		private int AlphaBeta(Color color, int depth, int alpha, int beta, Move move, int ply)
		{
			// TODO: IsInCheck
			depth--;
			if (depth == 0 || ply > MaxPly) return ForcedSearch(alpha, beta, ply, color); // TODO: forced search
			var moves = GetAvailableMoves(); // TODO: sort moves
			int result = int.MinValue;

			for (int i = 0; i < moves.Count; i++)
			{
				// TODO: if king was ate?
				MakeMove(moves[i]);
				var opColor = (color == Color.White) ? Color.Black : Color.White;
				int score = -1 * AlphaBeta(opColor, depth, -(alpha + 1), -alpha, move, ply + 1); // TODO: change move
				if (score > alpha && score < beta)
				{
					score = -1 * AlphaBeta(opColor, depth, -beta, -score, move, ply + 1); // TODO: change move
				}
				UnmakeMove(moves[i]);
				if (score > result) result = score;
				if (result > alpha)
				{
					alpha = result;
					// return bestmove
				}
				if (alpha >= beta) return alpha;
			}
			// TODO: check stalemate
			result = 0;
			return result;
		}

		private int ForcedSearch(int alpha, int beta, int ply, Color color)
		{
			int score = EvaluatePosition(color);
			int result = int.MinValue;
			if (score > result) result = score;
			if (result > alpha) alpha = result;
			if (alpha >= beta) return alpha;
			var captures = GetAvailableCaptures();

			for (int i = 0; i < captures.Count; i++)
			{
				MakeMove(captures[i]);
				var temp = -1 * ForcedSearch(-beta, -alpha, ply + 1, color); //TODO: remove color
				UnmakeMove(captures[i]);
				if (temp > result) result = temp;
				if (result > alpha) alpha = result;
				if (alpha >= beta) return alpha;
			}
			return result;
		}

		private List<Move> GetAvailableCaptures()
		{
			throw new NotImplementedException();
		}

		private void UnmakeMove(Move move)
		{
			throw new NotImplementedException();
		}

		public int EvaluatePosition(Color color)
		{
			var evaluator = new PositionEvaluator(_currentBoard);
			return evaluator.Evaluate(color);
		}

		private string MakeMove(Move move)
		{
			// TODO: pawn to figure
			// TODO: captures and moves
			// TODO: castling
			// TODO: en passant
			throw new NotImplementedException();
		}
	}
}
