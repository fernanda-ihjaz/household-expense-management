using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;

namespace HouseholdExpenseManagement.Application.Models.Transaction
{
    public class TransactionModel
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
