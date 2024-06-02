using AutoMapper;
using Common.Entity;

namespace Common.Extensions
{
    public static class BaseEntityExtension
    {
        private static IMapper _mapper;

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }
        public static T MapTo<T>(this BaseEntity<int> entity)
        {
            return _mapper.Map<T>(entity);
        }
    }
}
//}