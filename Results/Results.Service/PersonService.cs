using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Guid> CreatePersonAsync(IPerson person) => await _personRepository.CreatePersonAsync(person);

        public async Task<bool> UpdatePersonAsync(IPerson person) => await _personRepository.UpdatePersonAsync(person);
    }
}
