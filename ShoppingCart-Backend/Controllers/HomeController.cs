using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_Backend.Data;
using ShoppingCart_Backend.DTO;
using ShoppingCart_Backend.Model;

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
                                                  .Where(x => x.emailId == userd.emailId);
                if (userc == null)
                {
                    return BadRequest("User Not Found");
                }
                else if (!userc.Any(x => x.password == userd.password))
                {
                    return BadRequest("Incorrect Password");
                }
                else
                {

                    return Ok(userc.Select(x => x.customerName));

                    //return Ok(_context.packs);
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
            IEnumerable<Item> itemd = _context.items;
            if(itemd != null)
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
            if(emailId == null ) {
                return Ok(_context.orders.Where(x => x.emailId == emailId));

            }
            else
            {
                return BadRequest("Please provide emailId! or Cart Empty");
            }
            
        }

        [HttpPost("/AddToCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart([FromBody] AddtocartDTO addtocart)
        {
            Item item = _context.items
                                      .Where(x => x.Id == addtocart.itemId)
                                      .SingleOrDefault() ;
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
                _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("item does not exist");
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
                    var t = _context.orders.Remove(items);
                    _context.SaveChangesAsync();
                    return Ok(Json("success"));
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


    }
}
