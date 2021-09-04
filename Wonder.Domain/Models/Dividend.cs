using System;
using Wonder.Domain.Shared;

namespace Wonder.Domain.Models
{
    public class Dividend
    {
        public int IdTicket { get; set; }
        public TypeTicket TypeTicket { get; set; } 
        public TypePayment TypePayment { get; set; }
        public float Value { get; set; } 
        public DateTime DateCom { get; set; } 
        public DateTime PaymentDate { get; set; } 
    }
}