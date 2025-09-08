namespace Pharmacy_Management_System.Domains.Interfaces.Transactions
{
    public interface ITransactionUtil : IDisposable
    {
        Task BeginAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}
