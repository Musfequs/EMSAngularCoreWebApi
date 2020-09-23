using DataAccess.Interface;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Globalization;

namespace DataAccess
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly IConfiguration configuration;
        private string ConnStr = string.Empty;

        public EmployeeDataService(IConfiguration config)
        {
            configuration = config;
        }


        private string ConnectionString {
            get 
            {
                if (string.IsNullOrWhiteSpace(ConnStr))
                {
                    ConnStr = configuration.GetConnectionString("TestDB");

                }

                return ConnStr;
            }
        }



        public List<EmployeeDetails> GetAllEmployeeDetails()
        {
            List<EmployeeDetails> empDetails = new List<EmployeeDetails>();

            try
            {
                using (IDbConnection con = new SqlConnection(ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var data  = con.Query<EmployeeDetails>("GetAllEmployees", commandType: CommandType.StoredProcedure);

                    if (data != null && data.Any())
                        empDetails = data.ToList();
                }

            }
            catch
            {
                throw;
            }

            return empDetails;
        }

        public bool AddEmployee(EmployeeDetails emp)
        {
            bool result = false;

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();


                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@EmpId", emp.EmpId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@EmpName", emp.EmpName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@EmpAddress", emp.EmpAddress, DbType.String, ParameterDirection.Input);
                    parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    dbConnection.Execute("CreateEmployee", parameter, commandType: CommandType.StoredProcedure);
                    result = true;
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
