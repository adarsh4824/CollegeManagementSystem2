using CollegeManagement.Areas.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Areas.Areas.MvcDashboardIdentity.Controllers
{
    public class HomeController : BaseController
    {
        #region Construction

        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
