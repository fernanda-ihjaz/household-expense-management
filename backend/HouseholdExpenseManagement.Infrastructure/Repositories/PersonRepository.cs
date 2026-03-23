using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdExpenseManagement.Infrastructure.EntityFramework;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using Microsoft.EntityFrameworkCore;

namespace HouseholdExpenseManagement.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
        }

        public void Delete(Person person)
        {
            _context.Persons.Remove(person);
        }
    }
}
