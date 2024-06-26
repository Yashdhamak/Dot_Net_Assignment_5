using AutoMapper;
using Employee_Management_System.Models;

namespace Employee_Management_System.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeBasicDetails, EmployeeBasicDetails>();
            CreateMap<EmployeeAdditionalDetails, EmployeeAdditionalDetails>();
        }
    }
}
