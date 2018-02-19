﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Models;

namespace ASPPostOffice.Controllers
{
    public class HomeController : Controller
    {
        private CellRepository _cellRepository = new CellRepository();

        public ActionResult Index()
        {
            Grid grid = _cellRepository.GetGrid();
            Response.AddHeader("Refresh", "5");
            return View(grid);
        }

        public ActionResult ClickCell(int row, int x)
        {
            _cellRepository.ToggleCell(x, row);
            return RedirectToAction("Index");
        }
    }
}