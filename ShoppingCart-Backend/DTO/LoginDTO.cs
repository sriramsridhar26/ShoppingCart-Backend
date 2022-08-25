namespace ShoppingCart_Backend.DTO
{
    public class LoginDTO
    {
        public string emailId { get; set; }
        public string password { get; set; }


    }
    public class UserDTO : LoginDTO
    {
        public string customerName { get; set; }
    }
}
