namespace Wonder.Service.Contracts.DTO.Wallet
{
    
    public class InfoWalletDTO
    {
        public string IdUser { get; set; }
        public int IdWallet { get; set; }
        public int IdTicket { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double LastPrice { get; set; }
        public double AveragePrice { get; set; }
        public double Percent { get; set; }
        public int Amount { get; set; }
        public double TotalStock { get; set; }
    }
}