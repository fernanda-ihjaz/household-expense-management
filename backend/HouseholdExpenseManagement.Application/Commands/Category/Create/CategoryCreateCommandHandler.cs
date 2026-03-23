using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using MediatR;
using _Category = HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate.Category;

namespace HouseholdExpenseManagement.Application.Commands.Category.Create
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommandRequest, Result<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryCreateCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CategoryCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var categoryModel = request.CategoryModel;
            try
            {
                var category = new _Category(
                    categoryModel.Description,
                    categoryModel.PurposeId
                );

                await _categoryRepository.AddAsync(category);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Ok(category.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Fail(ex.Message);
            }
        }
    }
}