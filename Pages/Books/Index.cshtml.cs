using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MateciucRobert_Lab2.Data;
using MateciucRobert_Lab2.Models;
using Microsoft.Data.SqlClient;

namespace MateciucRobert_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly MateciucRobert_Lab2.Data.MateciucRobert_Lab2Context _context;

        public IndexModel(MateciucRobert_Lab2.Data.MateciucRobert_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync((int? id, int? categoryID, string sortOrder, string searchString)
        {
            Book = await _context.Book.ToListAsync();
            TitleSort = String.IsNullOrEmpty(sortOrder) ? " title_desc " : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;
            BookD.Books = await _ context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .asNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s => s.Author.FirstName.Contains(
               searchString)
               || s.Author.LastName.Contains(
               searchString)
               || s.Title.Contains(searchString));
                if (id != null )
            {
                 BookID = id.value _
                Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();
                        BookD.Categories = book.BookCategories.Select(s=>s.Category );
            }
            switch (sortOrder)
            {
                case " title_desc ":
                    BookD.Books = BookD.Books.OrderByDescending(s =>
                   s.Title);
                    break;
                case " author_desc ":
                    BookD.Books = BookD.Books.OrderByDescending(s =>
                   s.Author.FullName);
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s =>
                   s.Author.FullName);
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title
                   );
                    break;
            }
        }
       
}
