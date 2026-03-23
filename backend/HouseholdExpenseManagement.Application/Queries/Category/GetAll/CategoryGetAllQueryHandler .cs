using HouseholdExpenseManagement.Application.Models.Category;
using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Category.GetAll
{
    public class CategoryGetAllQueryHandler : IRequestHandler<CategoryGetAllQueryRequest, IEnumerable<CategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGetAllQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> Handle(CategoryGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories == null || !categories.Any()) 
            {
                return [];
            }

            return categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Description = c.Description,
                Purpose = ((int)c.Purpose)
            });
        }
    }
}