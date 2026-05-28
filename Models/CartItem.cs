using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeStoreLocalAPI.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CartId { get; set; }
        //[ForeignKey("FK_CartId")]
        public Cart Cart { get; set; } = new Cart();
        public int ProductId { get; set; }
        //[ForeignKey("FK_ProductId")]
        public Product Product { get; set; } = new Product();
        public float Quantity { get; set; }
    }
}
