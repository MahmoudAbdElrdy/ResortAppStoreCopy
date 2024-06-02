using AutoMapper;
using Common.Mapper;
using Configuration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Dto
{
    public class BeneficiariesGroupDetailsDto: IHaveCustomMapping
    {
         public long Id { get; set; }
        public long BeneficiaryGroupId { get; set; }

        public string? Code { get; set; }

        public string? EntitiesIds { get; set; }
        public string? EntityType { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BeneficiariesGroupDetailsDto, BeneficiariesGroupDetails>()
                .ReverseMap();
            configuration.CreateMap<BeneficiariesGroupDetails, BeneficiariesGroupDetailsDto>().ReverseMap();

        }
    }
}
 