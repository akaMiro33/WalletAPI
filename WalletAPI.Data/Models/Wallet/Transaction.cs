using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAPI.Data.Enums;

namespace WalletAPI.Data.Models.Wallet
{
    public class Transaction {

        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public bool Accepted { get; set; }
    }
}
