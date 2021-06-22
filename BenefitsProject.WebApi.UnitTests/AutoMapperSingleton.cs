using AutoMapper;
using BenefitsProject.WebApi.Models.Mappings;

namespace BenefitsProject.WebApi.UnitTests
{
    // adapted from https://mariomucalo.com/xunit-for-unit-testing-controllers-with-automapper-returns-mapper-already-initialized-error/
    public class AutoMapperSingleton
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var mappingConfig = new MapperConfiguration(configuration =>
                    {
                        configuration.AddProfile(new MappingProfile());
                    });

                    IMapper mapper = mappingConfig.CreateMapper();

                    _mapper = mapper;
                }

                return _mapper;
            }
        }
    }
}