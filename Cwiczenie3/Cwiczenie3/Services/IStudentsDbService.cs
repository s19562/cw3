using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenie3.Services
{
    public interface IStudentsDbService
    {
        public IActionResult StudentEnrolment(Student student);
        bool StudentExist(string dbs);
    }
}
