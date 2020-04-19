using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenie3.Services
{
    public class ServerDbService : ControllerBase, IStudentsDbService

    {
        String idStudies;
        public IActionResult StudentEnrolment(Student student)
        {
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19562;Integrated Security=True"))
                using(SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * drom Studies where name =@name";
                com.Parameters.AddWithValue("name", student.Studies);
                con.Open();
                idStudies = com.ExecuteReader()["IdStudy"].ToString();

                //if(dr.Read ......

                if (false)  //spr czy istn
                {
                    return NotFound();
                }


            }



            using(SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19562;Integrated Security=True"))
                using(SqlCommand com = new SqlCommand())
            {
                com.Connection = con;

                com.CommandText = "INSERT INTO Student (IndexNumber , FirstName , LastName , BirthDate , idEnrollment)" +
                    "VALUES(@IndexNumber, @FirstName , @LastName , @BirthDate , @idEnrollment); ";


                com.Parameters.AddWithValue("IndexNumber" , student.IndexNumber);
                com.Parameters.AddWithValue("FirstName" , student.FirstName);
                com.Parameters.AddWithValue("LastName" , student.LastName);
                com.Parameters.AddWithValue("BirthDate" , student.BirthDate);
                com.Parameters.AddWithValue("idEnrollment" , idStudies);

                con.Open();
                var dr = com.ExecuteReader();

                dr.Read();
                string idd = dr["IdEnrollment"].ToString();

                return Ok(dr[0].ToString() + "," + dr[1] + "," + dr[2]);

            }

            return Ok("");
            throw new NotImplementedException();

        }

        public bool StudentExist(string index)
        {

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {


                com.Connection = con;
                com.CommandText = $"select IndexNumber from Student where IndexNumber = '{index}'";
                con.Open();


                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }

                return false;
            }
        }
    }
}
