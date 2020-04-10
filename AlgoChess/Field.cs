using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	public class Field
	{
		public Field(Color color, Figure figure)
		{
			Color = color;
			Figure = figure;
		}

		public Color Color { get; set; }
		public Figure Figure { get; set; }
		public char FenCode
		{
			get
			{
				return (Color == Color.White) ? char.ToUpper((char)Figure) : (char)Figure;
			}
		}
	}
}
