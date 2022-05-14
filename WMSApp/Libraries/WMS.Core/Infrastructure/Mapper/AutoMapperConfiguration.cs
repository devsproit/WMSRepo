using AutoMapper;

namespace WMS.Core.Infrastructure.Mapper
{
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper { get; private set; }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration { get; private set; }

        /// <summary>
        /// Initialize mapper
        /// </summary>
        /// <param name="config"></param>
        public static void Init(MapperConfiguration config)
        {
            MapperConfiguration = config;
            Mapper = config.CreateMapper();
        }
    }
}
