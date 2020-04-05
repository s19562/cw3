using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie3.Controllers
{
    [Route("api/enrollents/promotions")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Enrollments()
        {
            return View();
        }
    }
}