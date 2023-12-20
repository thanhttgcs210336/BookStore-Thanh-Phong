using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Data.Migrations;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstBook = db.Book.AsNoTracking().OrderBy(x => x.BookName);
            var cartItems = GetCartItems();

            // If cartItems is null, create an empty list
            cartItems ??= new List<CartItem>();

            // PagedList<Book> creation
            PagedList<Book> lst = new PagedList<Book>(lstBook, pageNumber, pageSize);

            // Pass both the list of books and the cart items to the view
            ViewBag.CartItems = cartItems;
            return View(lst);
           
           
        }
        public const string CARTKEY = "cart";
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
        public IActionResult BookCategory(string id)
        {
            List<Book> category = db.Book.Where(x => x.CategoryId.Equals(id)).ToList();
            return View(category);
        }


        public IActionResult Bookdetail(string BookID)
        {
            var book = db.Book.SingleOrDefault(x => x.BookId == BookID);
            var books = db.Book.Where(x => x.BookId == BookID).ToList();
            ViewBag.books = book;
            return View(book);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
