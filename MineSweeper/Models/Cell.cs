using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MineSweeper.Models
{
	public class Cell
	{
		public int X { private set;  get; }
		public int Y { private set;  get; }
		public bool IsBomb;
		public CellState State;
		public int Number = 0; // Number of bombs around the cell
		public readonly Field MyField;

		public Cell(int x, int y, CellState state, Field myField)
		{
			X = x;
			Y = y;
			State = state;
			MyField = myField;
		}

		public void Open()
		{
			if(State == CellState.Opened)
				return;
			if(State != CellState.Marked)
				State = CellState.Opened;
			if(GameEnd())
				return; //End		
			//Implement style changes
		}

		public bool GameEnd()
		{
			if(State == CellState.Opened && IsBomb)
			{
				return true;
			}
			return false;
		}

		public void MarkOrUnMark()
		{
			if(State == CellState.Hidden)
			{
				State = CellState.Marked;
				if(IsBomb)
				{
					MyField.MarkedBombs++;
				}
			}
			else if(State == CellState.Marked)
			{
				State = CellState.Hidden;
				if(IsBomb)
				{
					MyField.MarkedBombs--;
				}
			}		
		}
	}

	public enum CellState
	{
		Hidden = 0,
		Marked = 1,
		Opened = 2
	}
}