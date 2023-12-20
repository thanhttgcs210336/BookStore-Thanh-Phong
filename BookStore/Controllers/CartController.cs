using BookStore.Data;
using BookStore.Data.Migrations;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext db;

       public CartController(ILogger<CartController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult AddToCart(string productid)
        {

            var cart = GetCartItems();
            var cartitem = cart.SingleOrDefault(p => p.book.BookId == productid);
            if (cartitem == null)
            {
                var product = db.Book.SingleOrDefault(p => p.BookId == productid);
                cart.Add(new CartItem() { quantity = 1, book = product });


            }
            else
            {
                cartitem.quantity++;

            }

            // Lưu cart vào Session
            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }

       
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] string productid)
        {

           
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.BookId == productid);
            if (cartitem != null)
            {
               
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        
        public IActionResult RemoveBook(string bookID)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.BookId == bookID);
            if (cartitem != null)
            {
                
                cartitem.quantity--;
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


    


        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        
        public IActionResult UpdateCart([FromRoute] string productid, [FromRoute] int quantity)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.BookId == productid);
            if (cartitem != null)
            {
             
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            
            return Ok();
        }
        // Hiện thị giỏ hàng
       
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
       
        [Route("/checkout")]
        public IActionResult CheckOut()
        {
           
            return View();
        }



        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

    }
}
