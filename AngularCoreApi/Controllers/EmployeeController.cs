using System.Collections.Generic;
using System.Linq;
using APiModel = AngularCoreApi.Model;
using DAccessModel = DataAccess.Model;
using AutoMapper;
using DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace AngularCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger;
        private readonly IEmployeeDataService employeeDataService;
        private readonly IMapper mapper;  

        public EmployeeController(ILogger<EmployeeController> log, IEmployeeDataService empDataService, IMapper autoMapper)
        {
            logger = log;
            employeeDataService = empDataService;
            mapper = autoMapper;
        }

        [HttpGet]
        [Route("api/v1/GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            logger.LogInformation("Start GetAllEmployees().");
            try
            {
                var empDetails = employeeDataService.GetAllEmployeeDetails();

                if (empDetails == null || !empDetails.Any())
                    return NotFound("No data found.");

                var apiEmpDetails = mapper.Map<List<APiModel.EmployeeDetails>>(empDetails);

                return new JsonResult(apiEmpDetails);
            }
            catch(Exception ex) 
            {
                logger.LogInformation("Exception occurs in GetAllEmployees(), Details: "+ ex.Message);
                return StatusCode(500, "Error Occurred");
            }
            finally 
            {
                logger.LogInformation("End GetAllEmployees().");
            }
        }

        [HttpPost]
        [Route("api/v1/CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] APiModel.EmployeeDetails employeeDetails)
        {
            logger.LogInformation("Start CreateEmployee().");
            try
            {
                if (employeeDetails == null)
                    return StatusCode(204, "EmployeeDetails is null.");

                var empDetails = mapper.Map<DAccessModel.EmployeeDetails>(employeeDetails);
                var result = employeeDataService.AddEmployee(empDetails);
                return new JsonResult(result);
            }
            catch(Exception ex)
            {
                logger.LogInformation("Exception occurs in CreateEmployee(), Details: " + ex.Message);
                return StatusCode(500, "Error Occurred");
            }
            finally
            {
                logger.LogInformation("End CreateEmployee().");
            }
        }
    }
}