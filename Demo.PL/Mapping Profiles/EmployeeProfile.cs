using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.Mapping_Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
                //ForMember (d => d.Name , O => O.MapFrom(s => s.EmpName)); // 3l4an lw esm el value fe viewmodel 8er esmha fe el model

        }

    }
}
