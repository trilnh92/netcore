using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}