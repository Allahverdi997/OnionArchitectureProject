using AutoMapper;
using RusMProject.Domain.Entities;
using RusMProjectApplication.DTOs;
using RusMProjectApplication.Models;
using RusMProjectApplication.Registration.CreateDSO;
using RusMProjectApplication.Registration.UpdateDSO;
using RusMProjectApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProjectApplication.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name));

            CreateMap<Employee, GetEmployeeResponseModel>()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name));

            CreateMap<Department, GetDepartmentResponseModel>();

            CreateMap<EmployeeDSO, Employee>();
            CreateMap<DepartmentDSO, Department>();

            CreateMap<EmployeeUpdateDSO, Employee>();
            CreateMap<DepartmentUpdateDSO, Department>();



        }
    }
}
