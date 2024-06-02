

namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class OrganizationDto
    {
        public long Id { get; set; }

        public string OrganizationName { get; set; }
        public string Username { get; set; }

        public string OrganizationNameEn { get; set; }

        public bool IsSubscriped { get; set; }

        public bool IsPaid { get; set; }
    }
}
