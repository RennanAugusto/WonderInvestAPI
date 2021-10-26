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
        public TypeTicket TypeTicket { get; set; }
        public int Amount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime SalesDate { get; set; }
    }
}