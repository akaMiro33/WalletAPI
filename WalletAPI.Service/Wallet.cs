using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAPI.Data.Enums;
using WalletAPI.Data.Models.Wallet;

namespace WalletAPI.Service
{
    public class Wallet
    {
        private decimal _balance;
        private readonly ConcurrentDictionary<Guid, Transaction> _transactions = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public decimal Balance => _balance;

        public async Task<bool> ProcessTransactionAsync(Guid transactionId, TransactionType type, decimal amount)
        {
            if (_transactions.ContainsKey(transactionId))
                return _transactions[transactionId].Accepted;

            await _semaphore.WaitAsync();
            try
            {
                if (_transactions.ContainsKey(transactionId))
                    return _transactions[transactionId].Accepted;

                decimal newBalance = _balance + GetAmountChange(type, amount);
                if (newBalance < 0)
                {
                    _transactions[transactionId] = new Transaction
                    {
                        Id = transactionId,
                        Type = type,
                        Amount = amount,
                        Accepted = false
                    };

                    return false;
                }

                _balance = newBalance;
                _transactions[transactionId] = new Transaction
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
                _semaphore.Release();
            }
        }

        public IEnumerable<Transaction> GetTransactions() => _transactions.Values;

        private static decimal GetAmountChange(TransactionType type, decimal amount) => type switch
        {
            TransactionType.Deposit => amount,
            TransactionType.Win => amount,
            TransactionType.Stake => -amount,
            _ => throw new ArgumentException("Invalid transaction type")
        };
    }
}
