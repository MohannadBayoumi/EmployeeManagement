﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BL.Interfaces
{
    public interface IAuthorizationBL
    {
        string Login(string username, string password);
    }
}
