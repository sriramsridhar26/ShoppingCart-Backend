using System.ComponentModel.DataAnnotations;

namespace ShoppingCart_Backend.Model
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string itemName { get; set; }
        public string Time { get; set; }
        public string description { get; set; }
        public int cost { get; set; }
        public string imglink { get; set; }
    }
}
