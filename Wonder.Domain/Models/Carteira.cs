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
            var averagePrice = ((lastRlcWallet.AveragePrice * lastRlcWallet.Amount) +
                               (this.Amount * price)) / (lastRlcWallet.Amount + this.Amount);
            return averagePrice;
        }
    }

    [Table("infoWallet")]
    public class InfoWallet 
    {
        [Column("IdInfo")]
        public int IdInfo { get; set; }
        
        [Column("Idusualo")]
        public string IdUsualo { get; set; }
        
        [Column("IdWallet")]
        public int IdWallet { get; set; }
        
        [Column("IdTicket")]
        public int IdTicket { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Code")]
        public string Code { get; set; }
        
        [Column("LastStockPrice")]
        public double LastStockPrice { get; set; }
        
        [Column("AveragePrice")]
        public double AveragePrice { get; set; }
        
        [Column("Percent")]
        public double Percent { get; set; }
        
        [Column("Amount")] 
        public int Amount { get; set; }
        
        [Column("TotalStock")]
        public double TotalStock { get; set; }
    }
}