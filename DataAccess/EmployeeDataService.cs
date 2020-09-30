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
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<EmployeeDataService> logger;
        private string ConnStr = string.Empty;

        public EmployeeDataService(ILogger<EmployeeDataService> log, IConfiguration config)
        {
            configuration = config;
            logger = log;
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDetails> GetAllEmployeeDetails()
        {
            logger.LogInformation("Start EmployeeDataService.GetAllEmployeeDetails().");
            List<EmployeeDetails> empDetails = new List<EmployeeDetails>();

            try
            {
                using (IDbConnection con = new SqlConnection(ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var data = con.Query<EmployeeDetails>("GetAllEmployees", commandType: CommandType.StoredProcedure);

                    if (data != null && data.Any())
                        empDetails = data.ToList();
                }
            }
            catch(Exception ex)
            {
                logger.LogInformation("Exception occurs in EmployeeDataService.GetAllEmployeeDetails(), Details: " + ex.Message);
                throw ex;
            }
            finally
            {
                logger.LogInformation("End EmployeeDataService.GetAllEmployeeDetails().");
            }

            
            return empDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeDetails emp)
        {
            bool result = false;
            logger.LogInformation("Start EmployeeDataService.AddEmployee().");
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@EmpId", emp.EmpId, DbType.String, ParameterDirection.Input);
                    parameter.Add("@EmpName", emp.EmpName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@EmpAddress", emp.EmpAddress, DbType.String, ParameterDirection.Input);
                    parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    dbConnection.Execute("CreateEmployee", parameter, commandType: CommandType.StoredProcedure);
                    result = true;
                }
            }
            catch(Exception ex)
            {
                logger.LogInformation("Exception occurs in EmployeeDataService.AddEmployee(), Details: " + ex.Message);
                throw ex;
            }
            finally
            {
                logger.LogInformation("End EmployeeDataService.AddEmployee().");
            }

            return result;
        }
    }
}
