using System.ComponentModel.DataAnnotations;

namespace BookingApi.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName {  get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
