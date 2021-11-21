using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public bool IsPurchase { get; set; }

        public float GetAveragePrice(RlcWalletTicket lastRlcWallet, float price)
        {
            if (lastRlcWallet.Amount <= 0)
                return price;
            
            var averagePrice = ((lastRlcWallet.AveragePrice * lastRlcWallet.Amount) +
                               (this.Amount * price)) / (lastRlcWallet.Amount + this.Amount);
            return averagePrice;
        }
    }

    [Table("infowallet")]
    public class InfoWallet 
    {
        [Key]
        [Column("idinfo")]
        public int IdInfo { get; set; }
        
        [Column("idusualo")]
        public string IdUsualo { get; set; }
        
        [Column("idwallet")]
        public int IdWallet { get; set; }
        
        [Column("idticket")]
        public int IdTicket { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("code")]
        public string Code { get; set; }
        
        [Column("laststockprice")]
        public double LastStockPrice { get; set; }
        
        [Column("averageprice")]
        public double AveragePrice { get; set; }
        
        [Column("percent")]
        public double Percent { get; set; }
        
        [Column("amount")] 
        public int Amount { get; set; }
        
        [Column("totalstock")]
        public double TotalStock { get; set; }
        
        [Column("totalvariation")]
        
        public double TotalVariation { get; set; }
        
        [Column("logobase64")]
        public string CompanyLogo { get; set; }
        
        [Column("lastoperation")]
        public string LastOperation { get; set; }
        
    }
}