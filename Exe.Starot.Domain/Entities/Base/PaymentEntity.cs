using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exe.Starot.Domain.Entities.Base
{
    public class PaymentEntity : Entity
    {
        [Key]
        public string PaymentId { get; set; }
        public string Method { get; set; } // ZaloPay, VNPay, etc.
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } // Completed, Pending, Failed
        public string TransactionId { get; set; }

        public virtual TransactionEntity Transaction { get; set; }
    }
}
