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

		private List<string> GetAvailableMoves()
		{
			// TODO: move representation?
			// TODO: get list of moves from current position.
			throw new NotImplementedException();
		}

		private int AlphaBeta(Color color, int depth, int alpha, int beta)
		{
			if (depth == 0) return EvaluatePosition(color);
			int score = int.MinValue;
			var moves = GetAvailableMoves();

			for (int i = 0; i < moves.Count; i++)
			{
				MakeMove(moves[i]);
				var opColor = (color == Color.White) ? Color.Black : Color.White;
				int value = -1 * AlphaBeta(opColor, depth - 1, -beta, -alpha);
				if (value > score) score = value;
				if (score > alpha) alpha = score;
				if (alpha >= beta) return alpha;
			}
			return score;
		}

		private int EvaluatePosition(Color color)
		{
			throw new NotImplementedException();
		}

		private string MakeMove(string v)
		{
			throw new NotImplementedException();
		}
	}
}
