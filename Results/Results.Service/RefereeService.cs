using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class RefereeService : IRefereeService
    {
        private readonly IRefereeRepository _refereeRepository;
        private readonly IPersonService _personService;

        public RefereeService(IRefereeRepository refereeRepository, IPersonService personService)
        {
            _refereeRepository = refereeRepository;
            _personService = personService;
        }

        public async Task<Guid> CreateRefereeAsync(IReferee referee)
        {
            referee.Id = await _personService.CreatePersonAsync(referee);
            await _refereeRepository.CreateRefereeAsync(referee);
            return referee.Id;
        }

        public async Task<bool> DeleteRefereeAsync(Guid id, Guid userId) => await _refereeRepository.DeleteRefereeAsync(id, userId);

        public async Task<IReferee> GetRefereeByIdAsync(Guid id) => await _refereeRepository.GetRefereeByIdAsync(id);

        public async Task<bool> UpdateRefereeAsync(IReferee referee)
        {
            if(!(await _personService.UpdatePersonAsync(referee)))
            {
                return false;
            }
            return await _refereeRepository.UpdateRefereeAsync(referee);

        }
    }
}
