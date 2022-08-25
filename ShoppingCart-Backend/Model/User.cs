using System.ComponentModel.DataAnnotations;

namespace ShoppingCart_Backend.Model
{
    public class User
    {
        [Key]
        [EmailAddress]
        public string emailId { get; set; }
        public string password { get; set; }
        public string customerName { get; set; }
    }
}
