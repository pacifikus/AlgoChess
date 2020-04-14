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
		private const int FieldCount = 256;
		private List<Figure> _figures;
		private string _fenSection;

		public Board(string fen)
		{
			_fenSection = fen;
			Fields = new int[FieldCount];

			FillBorderFields();
			FillDefaultFields();
			InitFields();
		}

		public List<Figure> Figures => _figures;

		private void FillDefaultFields()
		{
			for (int i = 4; i < 12; i++)
			{
				for (int j = 4; j < 12; j++)
				{
					int index = 16 * i + j - 1;
					Fields[index] = -1;
				}
			}
		}

		private void FillBorderFields()
		{
			for (int i = 0; i < Fields.Length; i++)
			{
				Fields[i] = int.MinValue;
			}
		}

		public int[] Fields { get; private set; }

		public List<Figure> GetFiguresByColor(Color color)
		{
			return _figures.Where(x => x.Color == color).ToList();
		}

		public string ToASCII()
		{
			int sideSize = 8;
			string ascii = "+---------------------+\n";

			for (int i = 4; i < 12; i++)
			{
				string row = $"{sideSize - i + 4} | ";
				for (int j = 4; j < 12; j++)
				{
					int index = 16 * i + j - 1;
					if (Fields[index] == -1)
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
			int figureIndex = Fields[index];
			return _figures[figureIndex].FenCode.ToString();
		}

		private void InitFields()
		{
			int[] temp = new int[64];
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
						int step = symbol - '0';
						for (int k = 0; k < step; k++)
						{
							temp[index] = -1;
							index++;
						}
					}
					else
					{
						var color = char.IsUpper(symbol) ? Color.White : Color.Black;
						var type = (FigureType)char.ToLower(symbol);
						AddFigure(index, color, type);
						temp[index] = _figures.Count - 1;
						index++;
					}
				}
			}

			Resize64To256Fields(temp);
		}

		private void AddFigure(int index, Color color, FigureType type)
		{
			var figure = new Figure()
			{
				Type = type,
				Position = (4 + index / 8) * 16 + index % 8 + 3,
				IsEnable = true,
				Color = color,
				IsMoved = false // TODO: if start position
			};
			_figures.Add(figure); // TODO: sort by weight?
		}

		private void Resize64To256Fields(int[] temp)
		{
			int indexTemp = 0;
			for (int i = 4; i < 12; i++)
			{
				for (int j = 4; j < 12; j++)
				{
					int indexFields = 16 * i + j - 1;
					Fields[indexFields] = temp[indexTemp];
					indexTemp++;
				}
			}
		}
	}
}
