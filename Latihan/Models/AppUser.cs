using System.ComponentModel.DataAnnotations;

namespace Latihan.Models
{
    public class AppUser
    {
        [Key]
        public String Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
