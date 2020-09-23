using DataAccess.Model;
using System;
using System.Collections.Generic;

namespace DataAccess.Interface
{
    public interface IEmployeeDataService
    {
        public List<EmployeeDetails> GetAllEmployeeDetails();

        public bool AddEmployee(EmployeeDetails emp);
        
    }
}
