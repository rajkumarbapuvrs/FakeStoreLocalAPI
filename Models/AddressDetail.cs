using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeStoreLocalAPI.Models
{
    public class AddressDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public int GeoLocationDetailId { get; set; }
        //[ForeignKey("FK_GeoLocationDetailId")]
        public GeoLocationDetail GeoLocation { get; set; } = new GeoLocationDetail();
        public string City { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
