using System;
using Microsoft.AspNetCore.Http.Connections;
using Wonder.Domain.Models;
using Wonder.Domain.Shared;

namespace Wonder.Domain.Models
{
    public class Wallet: Base
    {
        public string IdWonderUsers { get; set; }
    }

    public class RlcWalletTicket : Base
    {
        public int IdTicket { get; set; } 
        public int IdWallet { get; set; } 
        public DateTime OperationDate { get; set; }
        public int Amount { get; set; } 
        public float AveragePrice { get; set; }

        public float GetAveragePrice(RlcWalletTicket lastRlcWallet, float price)
        {
            var averagePrice = ((lastRlcWallet.AveragePrice * lastRlcWallet.Amount) +
                               (this.Amount * price)) / (lastRlcWallet.Amount + this.Amount);
            return averagePrice;
        }
    }

    public class Releases : Base
    {
        
    }
}