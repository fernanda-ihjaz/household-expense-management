using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Application.Models.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Purpose { get; set; }
    }
}
