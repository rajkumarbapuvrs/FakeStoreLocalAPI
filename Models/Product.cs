using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeStoreLocalAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public float Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        [NotMapped]
        public string Category { get; set; } = string.Empty;
        public int CategoryDetailId { get; set; }
        //[ForeignKey("FK_CategoryId")]
        public CategoryDetail CategoryDetail { get; set; } = new CategoryDetail();
        public string Image { get; set; } = string.Empty;
        public int RatingDetailId { get; set; }
        //[ForeignKey("FK_RatingDetailId")]
        public RatingDetail Rating{ get; set; } = new RatingDetail();
    }
}