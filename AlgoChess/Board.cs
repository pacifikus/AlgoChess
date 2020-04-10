using AlgoChess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoChess
{
	public class Board
	{
		private const int SideSize = 8;
		private const int FieldCount = 64;
		private Field[] _fields = new Field[FieldCount];
		private string _fenSection;

		public Board(string fen)
		{
			_fenSection = fen;
			InitFields();
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
					if (_fields[index] == null)
						row += ".";
					else
						row += _fields[index].FenCode;
					row += " ";
				}
				ascii += row;
				ascii += "|\n";
			}
			ascii += "+---------------------+\n";
			return ascii;
		}

		private void InitFields()
		{
			var rows = _fenSection.Split('/');
			int index = 0;

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
						var figure = (Figure)char.ToLower(symbol);
						_fields[index] = new Field(color, figure);
						index++;
					}
				}
			}
		}
	}
}
