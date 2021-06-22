using AutoMapper;
using BenefitsProject.Core.Domain.Entities;

namespace BenefitsProject.WebApi.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();

            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<Dependent, DependentReadDto>();

            CreateMap<DependentCreateDto, Dependent>();

            CreateMap<DependentUpdateDto, Dependent>();
        }
    }
}
