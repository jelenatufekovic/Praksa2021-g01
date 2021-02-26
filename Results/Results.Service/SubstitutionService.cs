using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Repository;
using Results.Repository.Common;
using Results.Service.Common;

namespace Results.Service
{
    public class SubstitutionService : ISubstitutionService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public SubstitutionService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> CreateSubstitutionAsync(ISubstitution substitution)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.CreateSubstitutionAsync(substitution);
        }
        public async Task<bool> UpdateSubstitutionAsync(ISubstitution substitution)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.UpdateSubstitutionAsync(substitution);
        }
        public async Task<bool> DeleteSubstitutionAsync(Guid id, Guid byUser)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.DeleteSubstitutionAsync(id, byUser);
        }
        public async Task<PagedList<ISubstitution>> GetSubstitutionsByQueryAsync(SubstitutionParameters parameters)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.GetSubstitutionsByQueryAsync(parameters);
        }
    }
}
