using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletAPI.Models
{
    public class Wallet
    {
        private ConcurrentDictionary<Guid, Transaction> transactions = new();
        private SemaphoreSlim semaphore = new(1, 1);

        public decimal Balance { get; set; }
        public IEnumerable<Transaction> GetTransactions() => transactions.Values;

        public async Task<bool> HandleTransactionAsync(Guid transactionId, TransactionType type, decimal amount)
        {
            if (transactions.ContainsKey(transactionId))
            {
                var accepted = transactions[transactionId].Accepted;
                return accepted;
            }

            await semaphore.WaitAsync();
            try
            {
                if (transactions.ContainsKey(transactionId))
                {
                    return transactions[transactionId].Accepted;
                }

                decimal newBalance = Balance + GetAmountChange(type, amount);
                if (newBalance < 0)
                {
                    transactions[transactionId] = new Transaction
                    {
                        Id = transactionId,
                        Type = type,
                        Amount = amount * 2,
                        Accepted = false
                    };

                    return false;
                }

                Balance = newBalance;
                transactions[transactionId] = new Transaction
                {
                    Id = transactionId,
                    Type = type,
                    Amount = amount,
                    Accepted = true
                }; ;
                return true;
            }
            finally
            {
                semaphore.Release();
            }
        }


        private static decimal GetAmountChange(TransactionType type, decimal amount) => type switch
        {
            TransactionType.Deposit => amount,
            TransactionType.Win => amount,
            TransactionType.Stake => -amount,
            _ => throw new ArgumentException("Invalid transaction type")
        };
    }
}
