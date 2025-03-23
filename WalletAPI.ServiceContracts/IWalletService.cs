using System.Transactions;
using WalletAPI.Data.Models.Wallet.RequestArgs;

namespace WalletAPI.ServiceContracts
{
    public interface  IWalletService
    {
        public bool RegisterWallet(Guid playerId);
        public decimal GetBalance(Guid playerId);
        public bool ProcessTransactionAsync(Guid playerId, TransactionRequest request);
        public IEnumerable<Transaction> GetTransactions(Guid playerId);
    }
}
