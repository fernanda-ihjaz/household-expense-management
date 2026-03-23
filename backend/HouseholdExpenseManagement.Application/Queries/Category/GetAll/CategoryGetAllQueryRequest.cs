using HouseholdExpenseManagement.Application.Models.Category;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Category.GetAll
{
    public class CategoryGetAllQueryRequest : IRequest<IEnumerable<CategoryViewModel>>
    {

    }
}