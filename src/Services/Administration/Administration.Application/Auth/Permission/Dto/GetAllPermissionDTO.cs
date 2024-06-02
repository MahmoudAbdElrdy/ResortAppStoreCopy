using AutoMapper;
using Common.Mapper;


namespace ResortAppStore.Services.Administration.Application.Auth.Permission.Dto
{
    public class GetAllPermissionDTO : IHaveCustomMapping
    {
        public long Id { set; get; }
        public string Name { get; set; }
        public string ControllerNameAr { set; get; }
        public string ControllerNameEn { set; get; }
        public string ActionNameAr { set; get; }
        public string ActionNameEn { set; get; }
        public bool? IsChecked { set; get; }
        public long? ScreenId { get; set; }
        public string RoleId { get; set; }
     
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AuthDomain.Entities.Auth.Permission, GetAllPermissionDTO>()
                 .ForMember(dest => dest.ControllerNameAr, opt => opt.MapFrom(s => s.Screen.ScreenNameAr))
                 .ForMember(dest => dest.ControllerNameEn, opt => opt.MapFrom(s => s.Screen.ScreenNameEn))
                // .ForMember(dest => dest.Screens, opt => opt.MapFrom(s => s.Screen))
                .ReverseMap();

        }
    }
}
