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

		private void InitGame()
		{
			string[] fenSections = CurrentPosition.Split(' ');
			_currentBoard = new Board(fenSections[0]);
			_turn = (Color)fenSections[1][0];
			// TODO: parse castling
		}

		

		public string CurrentPosition { get; set; }

		public void ResetBoard()
		{
			// TODO: currentPosition = START_POSITION, reset all
		}

		public void LoadFromFEN(string fen)
		{
			// TODO: currentPosition = parse(fen)
		}

		public string ToASCII()
		{
			return _currentBoard.ToASCII();
		}

		public void Undo()
		{
			// TODO: Undo last move.
		}

		private bool IsValidFEN(string fen)
		{
			// TODO: validate fen
			return false;
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

		private List<string> GetAvailableMoves()
		{
			// TODO: get list of moves from current position.
			throw new NotImplementedException();
		}
	}
}
