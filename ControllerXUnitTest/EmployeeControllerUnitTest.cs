using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace ControllerXUnitTest
{
    public class EmployeeControllerUnitTest : EmployeeControllerUnitTestBase
    {
        [Fact]
        public void EmployeeController_WhenEmpDetailsIsNull_ReturnNotFound()
        {
            //Arrange
            List<EmployeeDetails> empDetails = new List<EmployeeDetails>();
            MockEmployeeDataService.Setup(x => x.GetAllEmployeeDetails()).Returns(empDetails);

            // Act
            var response = Controller.GetAllEmployees();

            // Assert
    
            var responseReslt = response.ShouldBeOfType<NotFoundObjectResult>();
            responseReslt.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
            responseReslt.Value.ShouldBe("No data found.");

            MockEmployeeDataService.Verify(x => x.GetAllEmployeeDetails(), Times.Once);
            VerifyLoggerInvoked(true, "Start GetAllEmployees().", "End GetAllEmployees().");

        }
    }
}
