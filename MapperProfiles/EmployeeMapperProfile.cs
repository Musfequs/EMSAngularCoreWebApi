using ApiModel=AngularCoreApi.Model;
using AutoMapper;
using DAccessModel = DataAccess.Model;

namespace MapperProfiles
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<DAccessModel.EmployeeDetails, ApiModel.EmployeeDetails>().ReverseMap();
           ;
        }
    }
}
