using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MineSweeper.Models;

namespace MineSweeper.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

		public void CellLeftClick(string ButtonName)
		{
			Field GameField = (TempData["GameField"] as Field);

			GameField.Open(ButtonName);

			TempData["GameField"] = GameField;
		}

		public void CellRightClick(string ButtonName)
		{
			Field GameField = (TempData["GameField"] as Field);

			int[] index = ButtonName.Split('|').Select(x => Int32.Parse(x)).ToArray();

			GameField.Cells[(index[0], index[1])].MarkOrUnMark();

			TempData["GameField"] = GameField;
		}
	}
}