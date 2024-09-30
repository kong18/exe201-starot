using Exe.Starot.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderEntity : Entity
{
    public string Code { get; set; }
    public string UserId { get; set; }
    public string Status { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentStatus { get; set; } // Pending, Paid, Failed

    public virtual UserEntity User { get; set; }
    public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
    //public virtual ICollection<TransactionEntity> Transactions { get; set; }
}
