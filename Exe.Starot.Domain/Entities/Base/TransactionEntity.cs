//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Exe.Starot.Domain.Entities.Base
//{
//    public class TransactionEntity : Entity
//    {
//        public string OrderId { get; set; }
//        [Column(TypeName = "decimal(18,2)")]
//        public decimal Amount { get; set; }
//        public DateTime TransactionDate { get; set; }
//        public string TransactionCode { get; set; }  // e.g., ZaloPay transaction ID
//        public string Status { get; set; } // Pending, Success, Failed

//        public virtual OrderEntity Order { get; set; }
//        public virtual PaymentEntity Payment { get; set; }
//    }
//}
