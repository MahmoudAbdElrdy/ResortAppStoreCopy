using AutoMapper;
using ResortAppStore.Repositories;
 using Configuration.Entities;
using Configuration.Repository;

namespace Configuration.Repository
{
    public class BeneficiariesRepository : IBeneficiariesRepository
    {

        public BeneficiariesRepository(IMapper mapper,
            IGRepository<Beneficiaries> context)
        {

        }


    }
    
    
}
