using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess.Entities
{
	public struct Move
	{
		private int _position;
		private int _nextPosition;

		public Move(int position, int nextPosition) : this()
		{
			_position = position;
			_nextPosition = nextPosition;
		}

		public byte From { get; set; }
		public byte To { get; set; }
	}
}
