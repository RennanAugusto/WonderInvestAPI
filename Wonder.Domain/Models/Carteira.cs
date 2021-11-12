using System;
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
    }

    public class Releases : Base
    {
        
    }
}