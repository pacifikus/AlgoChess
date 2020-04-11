using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess.Entities
{
	public class Board
	{
		private const int SideSize = 8;
		private const int FieldCount = 64;
		private List<Figure> _figures;
		private byte[] _fields = new byte[FieldCount];
		private string _fenSection;

		public Board(string fen)
		{
			_fenSection = fen;
			InitFields();
		}

		public List<Figure> GetFiguresByColor(Color color)
		{
			return _figures.Where(x => x.Color == color).ToList();
		}

		public string ToASCII()
		{
			int sideSize = 8;
			string ascii = "+---------------------+\n";

			for (int i = 0; i < sideSize; i++)
			{
				string row = $"{sideSize - i} | ";
				for (int j = 0; j < sideSize; j++)
				{
					int index = i * sideSize + j;
					if (_fields[index] == 0)
						row += ".";
					else
						row += GetFEN(index);
					row += " ";
				}
				ascii += row;
				ascii += "|\n";
			}
			ascii += "+---------------------+\n";
			return ascii;
		}

		private string GetFEN(int index)
		{
			int figureIndex = _fields[index] - 1;
			return _figures[figureIndex].FenCode.ToString();
		}

		private void InitFields()
		{
			var rows = _fenSection.Split('/');
			int index = 0;
			_figures = new List<Figure>();

			for (int i = 0; i < SideSize; i++)
			{
				for (int j = 0; j < rows[i].Length; j++)
				{
					char symbol = rows[i][j];
					if (char.IsDigit(symbol))
					{
						index += symbol - '0'; 
					}
					else
					{
						var color = char.IsUpper(symbol) ? Color.White : Color.Black;
						var type = (FigureType)char.ToLower(symbol);
						var figure = new Figure()
						{
							Type = type,
							Position = index,
							IsEnable = true,
							Color = color,
							IsMoved = false // TODO: if start position
						};
						_figures.Add(figure);
						_fields[index] = (byte)_figures.Count;
						index++;
					}
				}
			}
		}
	}
}
