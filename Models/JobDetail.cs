using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeStoreLocalAPI.Models
{
    public class JobDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string RefNo { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Salary { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string JoiningDetail { get; set; } = string.Empty;
    }
}
