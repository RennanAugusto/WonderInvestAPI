using System;

namespace Wonder.Service.Contracts.DTO
{
    public class RlcWalletDTO
    {
        public string IdUser { get; set; }
        public int IdCarteira { get; set; }
        public int IdTicket { get; set; }
        public int Amount { get; set; }
        public bool Purchase { get; set; }
        public DateTime OperationDate { get; set; }
        public float Price { get; set; }
    }
}