using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	public class Figure
	{
		public FigureType Type { get; set; }
		public int Position { get; set; }
		public bool IsMoved { get; set; }
		public bool IsEnable { get; set; }
		public Color Color { get; set; }
		public char FenCode
		{
			get
			{
				return (Color == Color.White) ? char.ToUpper((char)Type) : (char)Type;
			}
		}
	}
}
