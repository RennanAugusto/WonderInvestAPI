using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Wonder.Domain.Models
{
    public class StockProgression
    {
        [Column("idp")]
        public int Id { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("price")]
        public double Price { get; set; }
        
        [Column("date")]
        public DateTime Date { get; set; }
        
        [Column("code")]
        public string Code { get; set; }
        
        [Column("stockid")]
        public int StockId { get; set; }
        
        [Column("legend")]
        public string Legend { get; set; }
        
        [Column("typeprogression")]
        public string TypeProgression { get; set; }
    }
}