using AngularCoreApi.Controllers;
using AutoMapper;
using DataAccess.Interface;
using MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;


namespace ControllerXUnitTest
{
    public class EmployeeControllerUnitTestBase : ControllerUnitTestBase<EmployeeController>
    {
        //protected Mock<ILogger<EmployeeController>> Logger { get; set; }
        protected Mock<IEmployeeDataService> MockEmployeeDataService { get; private set; } = new Mock<IEmployeeDataService>();
 
        protected override void ConfigureMapper(IMapperConfigurationExpression config)
        {
            config.AddProfile<EmployeeMapperProfile>();
            base.ConfigureMapper(config);
        }

        protected override void OnRegister(ServiceCollection services)
        {
            services.AddScoped(provider => MockEmployeeDataService.Object);
            services.AddScoped<EmployeeController>();
            base.OnRegister(services);
        }

        protected virtual void PostRegister()
        {

            //EmployeeDataService = 
        }

    }
}
