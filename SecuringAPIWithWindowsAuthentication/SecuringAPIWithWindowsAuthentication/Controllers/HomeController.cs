﻿using Microsoft.AspNetCore.Mvc;
using SecuringAPIWithWindowsAuthentication.Models;
using System.Diagnostics;

namespace SecuringAPIWithWindowsAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();            
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
