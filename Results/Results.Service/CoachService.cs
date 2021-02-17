using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepository _coachRepository;
        private readonly IPersonService _personService;

        public CoachService(ICoachRepository coachRepository, IPersonService personService)
        {
            _coachRepository = coachRepository;
            _personService = personService;
        }

        public async Task<Guid> CreateCoachAsync(ICoach coach)
        {
            coach.Id = await _personService.CreatePersonAsync(coach);
            await _coachRepository.CreateCoachAsync(coach);
            return coach.Id;
        }

        public async Task<bool> DeleteCoachAsync(Guid id, Guid userId) => await _coachRepository.DeleteCoachAsync(id, userId);

        public async Task<ICoach> GetCoachByIdAsync(Guid id) => await _coachRepository.GetCoachByIdAsync(id);

        public async Task<bool> UpdateCoachAsync(ICoach coach)
        {
            if (!(await _personService.UpdatePersonAsync(coach)))
            {
                return false;
            }
            return await _coachRepository.UpdateCoachAsync(coach);
        }
    }
}
