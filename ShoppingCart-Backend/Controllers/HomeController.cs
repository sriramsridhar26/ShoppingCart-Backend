using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_Backend.Data;
using ShoppingCart_Backend.DTO;
using ShoppingCart_Backend.Model;
using System.IO;

namespace ShoppingCart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginDTO userd)
        {
            if (userd.emailId == null || userd.password == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<User> userc = _context.users
                                                  .Where(x => x.emailId == userd.emailId && x.password == userd.password);
                if (userc == null)
                {
                    return BadRequest("Username or Password Incorrect");
                }
                else
                {

                    return Ok(userc.Select(x => x.customerName));
                }
            }

        }

        [HttpGet("/Getcustname")]
        public async Task<IActionResult> Getcustname([FromQuery] string emailId)
        {
            if (emailId != null)
            {
                IEnumerable<User> userc = _context.users
                                                  .Where(x => x.emailId == emailId);
                return Ok(userc.Select(x => x.customerName));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/GetItems")]
        public async Task<IActionResult> GetItems()
        {
            List<Item> itemd = _context.items.ToList();
            foreach (Item item in itemd)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(item.imglink);
                item.imglink = Convert.ToBase64String(bytes);

            }
            if (itemd != null)
            {
                return Ok(itemd);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("/GetOrders")]
        public async Task<IActionResult> GetOrder([FromQuery] string emailId)
        {
            if(emailId != null ) {
               // IEnumerable<Order> orders = _context.orders.Where(x => x.emailId == emailId);
                return Ok(_context.orders.Where(x => x.emailId == emailId));

            }
            else
            {
                return BadRequest("Please provide emailId! or Cart Empty");
            }
            
        }

        [HttpPost("/AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddtocartDTO addtocart)
        {
            var temp1 = _context.orders.Where(x => x.itemId == addtocart.itemId && x.purchased == false).FirstOrDefault();
            if (temp1 != null)
            {
                using (_context)
                {
                    temp1.quantity += 1;
                    _context.SaveChanges();
                }
                return Ok();
            }
            else
            {
                Item item = _context.items
                                          .Where(x => x.Id == addtocart.itemId)
                                          .SingleOrDefault();
                if (item != null)
                {
                    var t = new Order
                    {
                        emailId = addtocart.emailId,
                        itemId = addtocart.itemId,
                        itemName = item.itemName,
                        quantity = 1,
                        cost = item.cost,
                        purchased = false

                    };
                    _context.orders.Add(t);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("item does not exist");
                }
            }

        }
        [HttpDelete("/Deletecartitem")]
        public async Task<IActionResult> Deletecartitem([FromQuery] int orderid)
        {
            if (orderid != null)
            {
                var items = _context.orders.Where(x => x.orderId == orderid).FirstOrDefault();
                if (items != null)
                {
                    if (items.quantity > 1)
                    {
                        using (_context)
                        {
                            items.quantity -=1;
                            _context.SaveChanges();
                        }
                        
                        return Ok(Json("success"));
                    }
                    else
                    {
                        var t = _context.orders.Remove(items);
                        _context.SaveChangesAsync();
                        return Ok(Json("success"));
                    }

                }
                else
                {
                    return BadRequest("Item not present in cart");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromQuery] string emailId)
        {

            if (emailId != null)
            {
                using (_context)
                {
                    var temp = _context.orders.Where(x => x.emailId == emailId).ToList();
                    temp.ForEach(a =>
                    {
                        a.purchased = true;
                        a.purchaseDT = DateTime.Now;
                        a.paymentMode = "card";
                    });
                    _context.SaveChanges();
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/Numberincart")]
        public async Task<IActionResult> NumberinCart([FromQuery] string emailId)
        {
            if(emailId != null) 
            {
                int count = _context.orders.Where(p => p.emailId == emailId && p.purchased == false).Count();
                return Ok(count);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("/Imagetest")]
        public async Task<IActionResult> imgTest()
        {
            string Path = "C:/Users/Gideon/Desktop/IMG_20201026_231125.jpg";
            byte[] ig = System.IO.File.ReadAllBytes(Path);
            string img = Convert.ToBase64String(ig);
            return Ok(img);
        }
    }
}
