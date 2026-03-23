using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
