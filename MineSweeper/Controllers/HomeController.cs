using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MineSweeper.Models;
using System.Threading;

namespace MineSweeper.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			//ViewBag.Size = 10;
			return View();
		}

		public ActionResult Game(GameState gameMode = GameState.Easy)
		{
			Field GameField = new Field(gameMode);
			
			GameField.GetNumber();

			ViewBag.Size = Field.GameMode[gameMode].size;

			TempData["GameField"] = GameField;
			return View("Game");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View("Game");
		}

		

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}