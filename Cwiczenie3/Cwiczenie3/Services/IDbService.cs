﻿using System;
using System.Collections.Generic;
using Cwiczenie3.Models;

namespace Cwiczenie3.Services
{
    public interface IDbService

    {
        public IEnumerable<Student> GetStudents();
    }

}
