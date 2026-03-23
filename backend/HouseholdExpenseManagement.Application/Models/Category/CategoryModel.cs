using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Application.Models.Category
{
    public class CategoryModel
    {
        public string Description { get; set; } = string.Empty;
        public int PurposeId { get; set; }
    }
}
