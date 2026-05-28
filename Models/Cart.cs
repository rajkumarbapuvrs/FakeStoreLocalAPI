using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FakeStoreLocalAPI.DataBase;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreLocalAPI.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        //[ForeignKey("FK_UserId")]
        public User User { get; set; } = new User();
        public DateTime Date { get; set; }

        public List<CartItem> Products { get; set; } = new List<CartItem>();
    }
}
