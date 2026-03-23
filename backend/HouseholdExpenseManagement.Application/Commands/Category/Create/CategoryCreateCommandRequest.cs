using HouseholdExpenseManagement.Application.Models.Category;
using HouseholdExpenseManagement.Application.Results;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Category.Create
{
    public class CategoryCreateCommandRequest : IRequest<Result<Guid>>
    {
        public CategoryModel CategoryModel { get; set; }

        public CategoryCreateCommandRequest(CategoryModel categoryModel)
        {
            CategoryModel = categoryModel;
        }
    }
}
