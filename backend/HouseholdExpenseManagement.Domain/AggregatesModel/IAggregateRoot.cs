namespace HouseholdExpenseManagement.Domain.AggregatesModel
{
    public interface IAggregateRoot { }
    public interface IRepository<T> where T : IAggregateRoot
    {

    }
}
