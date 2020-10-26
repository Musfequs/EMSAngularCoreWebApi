using AutoMapper;
using DataAccess.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ControllerXUnitTest
{
    public class ControllerUnitTestBase<TController>
    {
        protected Mock<ILogger<TController>> MockLogger { get; set; } = new Mock<ILogger<TController>>();
        // protected Mock<IEmployeeDataService> EmployeeDataService { get; set; }
        // protected Mock<IMapper> Mapper { get; set; }
        protected Mapper Mapper { get; private set; }
        protected ServiceProvider ServiceProvider { get; private set; }
        protected TController Controller { get; private set; }

        public ControllerUnitTestBase()
        {
            Register();
            Controller = ServiceProvider.GetService<TController>();
        }

        protected virtual void PreRegister()
        {


        }
        private void Register()
        {
            var services = new ServiceCollection();
            services.AddScoped(provider => MockLogger.Object);

            var configuration = new MapperConfiguration(config =>
                {
                    ConfigureMapper(config);
                }
            );

            configuration.AssertConfigurationIsValid();
            Mapper = new Mapper(configuration);
            services.AddScoped<IMapper>(provider => Mapper);

            OnRegister(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected virtual void ConfigureMapper(IMapperConfigurationExpression mapper)
        {

        }

        protected virtual void OnRegister(ServiceCollection services)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddScoped<IConfiguration>(provider => config);
        }

        protected void VerifyLoggerInvoked(bool invoked, params string [] expectedMessages)
        {
            if (invoked)
            {
                var actualLogMessages = MockLogger.Invocations
                    .Where(x=>x.Arguments.Count>2)
                    .Select(x=>x.Arguments[2].ToString());

                actualLogMessages.ShouldBe(expectedMessages);
            }
            else
            {
                MockLogger.Invocations.Count.ShouldBe(0);
            }
        }

    }
}
