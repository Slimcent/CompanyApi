using AutoMapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>();
            CreateMap<PostCompanyDto, Company>();
            CreateMap<PostEmployeeDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<CompanyUpdateDto, Company>();
        }
    }
}
