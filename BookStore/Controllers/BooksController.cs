using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Data;
using BookStore.Data.Migrations;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext db;

        public BooksController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return db.Book != null ? 
                          View(await db.Book.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Book'  is null.");
        }

        // GET: Books/Details/5
     

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookName,CategoryId,Image,Author,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || db.Book == null)
            {
                return NotFound();
            }

            var book = await db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookId,BookName,CategoryId,Image,Author,Price")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(book);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || db.Book == null)
            {
                return NotFound();
            }

            var book = await db.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (db.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await db.Book.FindAsync(id);
            if (book != null)
            {
                db.Book.Remove(book);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
          return (db.Book?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
