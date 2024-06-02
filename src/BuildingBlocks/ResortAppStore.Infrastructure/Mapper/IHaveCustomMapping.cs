using AutoMapper;

namespace Common.Mapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}