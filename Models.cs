using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BlazorApp1
{
    public class User
    {
        //пабілк для зчитування бд
        public int Id { get; set; } //ID користувача
        [Required]
        public string Username { get; set; } // Ім'я
        [Required]
        public string PasswordHash { get; set; } //хеш
		public List<Trip> Trips { get; set; } = new List<Trip>();
    }

    public class Trip
    {
        public int Id { get; set; } 
        public int UserId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "";

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = "";

        public string Notes { get; set; } = "";

        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    }


}
