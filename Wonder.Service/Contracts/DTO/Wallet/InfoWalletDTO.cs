using System.Collections.Generic;

namespace Wonder.Service.Contracts.DTO.Wallet
{

    public class InfoWalletDTO
    {
        public double TotalWalletValue { get; set; }
        public double TotalTicketsValue { get; set; }
        public double PercentWallet { get; set; }
        public IList<InfoTicketDTO> ListInfoTicket { get; set; }
        public InfoWalletDTO()
        {
            this.ListInfoTicket = new List<InfoTicketDTO>();
        }
    }
    public class InfoTicketDTO
    {
        public string IdUser { get; set; }
        public int IdWallet { get; set; }
        public int IdTicket { get; set; }
        public string Name { get; set; }
        public string CompanyLogo { get; set; }
        public string LastOperation { get; set; }
        public string CompanyName { get; set; }
        public double LastPrice { get; set; }
        public double AveragePrice { get; set; }
        public double Percent { get; set; }
        public int Amount { get; set; }
        public double TotalStock { get; set; }
        public double TotalVariation { get; set; }
    }
}