using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenie3.Models;
//using Cwiczenie3.Serivices;
using Cwiczenie3.DAL;
using Microsoft.AspNetCore.Mvc;



namespace Cwiczenie3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IDbService _dbService;

        public StudentsController(IDbService service)
        {
            _dbService = service;
        }

        //2. QueryString
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            /*if (orderBy == "lastname")
            {
                return Ok(_dbService.GetStudents().OrderBy(s => s.LastName));
            }*/
            return Ok(_dbService.GetStudents());
        }

        //[FromRoute], [FromBody], [FromQuery]
        //1. URL segment
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute]int id) //action method
        {
            if(id <= _dbService.GetStudents().Count())
            {
                return Ok(_dbService.GetStudents().Where(student =>
                student.IdStudent == id));
            }
            else
                return NotFound("Student was not found");
        }

        //3. Body - cialo zadan
        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            //...
            return Ok(student); //JSON 
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent([FromRoute]int id )
        { 
            return Ok("Aktualizacja zakonczona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute]int id)
        {
            return Ok("Usuwanie ukonczone");
        }
    }
}
