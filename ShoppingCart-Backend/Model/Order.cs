using System.ComponentModel.DataAnnotations;

namespace ShoppingCart_Backend.Model
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        
        public string emailId { get; set; }
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public int cost { get; set; }
       
        public bool purchased { get; set; }
        public DateTime purchaseDT { get; set; }
    }
}
