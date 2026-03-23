using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Domain.AggregatesModel.Person
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person?> GetByIdAsync(Guid id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        void Update(Person person);
        void Delete(Person person);
    }
}
