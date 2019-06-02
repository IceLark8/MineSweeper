using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MineSweeper.Models
{
	public enum GameState
	{
		Easy = 1,
		Medium,
		Hard
	}
	public class Field
	{
		public static Dictionary<GameState, (int size, int bombs)> GameMode = new Dictionary<GameState, (int, int)> { [GameState.Easy] = (10, 10), [GameState.Medium] = (18, 40), [GameState.Hard] = (24, 99), };

		public Dictionary<ValueTuple<int,int>, Cell> Cells;
		public List<int> Bombs;
		public GameState State { get; set; }

		public byte MarkedBombs = 0;
		public Field(GameState state)
		{
			Cells = new Dictionary<(int, int), Cell>() { };
			State = state;
			for(int i = 0; i < GameMode[state].size; i++)
			{
				for(int j = 0; j < GameMode[state].size; j++)
				{
					Cells.Add((i, j), new Cell(i, j, CellState.Hidden, this));
				}
			}

			var a = GameMode[state].bombs;
			Bombs = new List<int>() { };
			Random rand = new Random();
			int random = rand.Next(1, GameMode[state].bombs);

			for(int i = 0; i < GameMode[state].bombs; i++)
			{
				a++;
				while(Bombs.Contains(random))
				{
					random = rand.Next(1, GameMode[state].size * GameMode[state].size - 1);
				}
				Bombs.Add(random);
			}

			foreach(int bomb in Bombs)
			{
				Cells[((bomb / GameMode[state].size), (bomb % GameMode[state].size))].IsBomb = true;
			}
		}

		public void Open(string ButtonName)
		{
			int[] index = ButtonName.Split('|').Select(x => Int32.Parse(x)).ToArray();


			OpenCell(Cells[(index[0], index[1])]);
		}

		public void OpenCell(Cell a)
		{
			a.Open();

			if(a.Number == 0)
			{
				int posx = a.X - 1;
				int posy = a.Y - 1;


				for(int i = 0; i < 3; i++)
				{
					if(posx < 0)
						i++;

					if(posx + i == GameMode[State].size)
						posx -= 1;

					for(int j = 0; j < 3; j++)
					{
						if(posy < 0)
							j++;

						if(posy + j == GameMode[State].size)
							posy -= 1;

						OpenCell(Cells[(posx + i, posy + j)]);
					}
				}
			}
			else
			{
				return;
			}
		}

		
		public void GetNumber()
		{

			foreach(var a in Cells.Keys)
			{
				int posx = a.Item1 - 1;
				int posy = a.Item2 - 1;


				for(int i = 0; i < 3; i++)
				{
					if(posx < 0)
						i++;

					if(posx + i == GameMode[State].size)
						posx -= 1;

					for(int j = 0; j < 3; j++)
					{
						if(posy < 0)
							j++;

						if(posy + j == GameMode[State].size)
							posy -= 1;

						if(Cells[(posx + i, posy + j)].IsBomb)
							Cells[a].Number++;
					}
				}
				
			}
		}


	}
}