using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APiModel = AngularCoreApi.Model;
using DAccessModel = DataAccess.Model;
using AutoMapper;
using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace AngularCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IEmployeeDataService employeeDataService;
        private readonly IMapper mapper;  

        public EmployeeController(IConfiguration config, IEmployeeDataService empDataService, IMapper autoMapper)
        {
            configuration = config;
            employeeDataService = empDataService;
            mapper = autoMapper;
        }

        [HttpGet]
        [Route("api/v1/GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var empDetails = employeeDataService.GetAllEmployeeDetails();

                if (empDetails == null || !empDetails.Any())
                    return NotFound("No data found.");

                var apiEmpDetails = mapper.Map<List<APiModel.EmployeeDetails>>(empDetails);

                return new JsonResult(apiEmpDetails);
            }
            catch 
            {
                return StatusCode(500, "Error Occurred");
            }
        }

        [HttpPost]
        [Route("api/v1/CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] APiModel.EmployeeDetails employeeDetails)
        {
            try
            {
                if (employeeDetails == null)
                    return StatusCode(204, "EmployeeDetails is null.");

                var empDetails = mapper.Map<DAccessModel.EmployeeDetails>(employeeDetails);
                var result = employeeDataService.AddEmployee(empDetails);
                return new JsonResult(result);
            }
            catch
            {
                return StatusCode(500, "Error Occurred");
            }
        }
    }
}