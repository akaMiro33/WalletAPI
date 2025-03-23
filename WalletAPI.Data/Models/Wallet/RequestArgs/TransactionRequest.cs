using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAPI.Data.Enums;

namespace WalletAPI.Data.Models.Wallet.RequestArgs
{
    public class TransactionRequest
    {
        public Guid TransactionId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
