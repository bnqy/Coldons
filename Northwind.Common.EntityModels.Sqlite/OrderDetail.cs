using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coldons.Lib
{
    [Table("Order Details")]
    [Index(nameof(OrderId), Name = "OrderId")]
    [Index(nameof(OrderId), Name = "OrdersOrder_Details")]
    [Index(nameof(ProductId), Name = "ProductId")]
    [Index(nameof(ProductId), Name = "ProductsOrder_Details")]
    public partial class OrderDetail
    {
        [Key]
        [Column(TypeName = "int")]
        public long OrderId { get; set; }
        [Key]
        [Column(TypeName = "int")]
        public long ProductId { get; set; }
        [Column(TypeName = "money")]
        public byte[] UnitPrice { get; set; } = null!;
        [Column(TypeName = "smallint")]
        public long Quantity { get; set; }
        [Column(TypeName = "real")]
        public double Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; } = null!;
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; } = null!;
    }
}
