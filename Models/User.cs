using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeStoreLocalAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int NameDetailId { get; set; }
        //[ForeignKey("FK_NameDetailId")]
        public NameDetail Name { get; set; } = new NameDetail();
        public int AddressId { get; set; }
        //[ForeignKey("FK_AddressDetailId")]
        public AddressDetail Address { get; set; } = new AddressDetail();
        public string Phone { get; set; } = string.Empty;
    }
}