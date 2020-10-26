using AngularCoreApi.Model;
using DataAccessModel = DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;
using FluentAssertions;

namespace ControllerXUnitTest
{
    public class EmployeeControllerUnitTest : EmployeeControllerUnitTestBase
    {
        [Fact]
        public void EmployeeController_WhenEmpDetailsIsNull_ReturnNotFound()
        {
            //Arrange
            List<DataAccessModel.EmployeeDetails> expectedEmpDetails = new List<DataAccessModel.EmployeeDetails>();
            MockEmployeeDataService.Setup(x => x.GetAllEmployeeDetails()).Returns(expectedEmpDetails);

            // Act
            var response = Controller.GetAllEmployees();

            // Assert
            var responseReslt = response.ShouldBeOfType<NotFoundObjectResult>();
            responseReslt.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
            responseReslt.Value.ShouldBe("No data found.");

            MockEmployeeDataService.Verify(x => x.GetAllEmployeeDetails(), Times.Once);
            VerifyLoggerInvoked(true, "Start GetAllEmployees().", "End GetAllEmployees().");
        }

        [Fact]
        public void EmployeeController_WhenEmpDetailsExist_ReturnListOfEmployees()
        {
            //Arrange
            List<DataAccessModel.EmployeeDetails> expectedEmpDetails = new List<DataAccessModel.EmployeeDetails>();
            expectedEmpDetails.Add(new DataAccessModel.EmployeeDetails
            { 
                    EmpId="123",
                    EmpName="John",
                    EmpAddress = "NSW, Australia"
                });

            MockEmployeeDataService.Setup(x => x.GetAllEmployeeDetails()).Returns(expectedEmpDetails);

            // Act
            var response = Controller.GetAllEmployees();

            // Assert
            var responseReslt = response.ShouldBeOfType<OkObjectResult>();
            responseReslt.StatusCode.ShouldBe((int)HttpStatusCode.OK);

            var jsonData = responseReslt.Value.ShouldBeOfType<JsonResult>();
            var actualEmployeeList = jsonData.Value.ShouldBeOfType<List<EmployeeDetails>>();
            actualEmployeeList.Should().BeEquivalentTo(expectedEmpDetails);

            MockEmployeeDataService.Verify(x => x.GetAllEmployeeDetails(), Times.Once);
            VerifyLoggerInvoked(true, "Start GetAllEmployees().", "End GetAllEmployees().");
        }
    }
}
