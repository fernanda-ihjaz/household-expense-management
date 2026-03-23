using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Application.Models.Transaction
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
