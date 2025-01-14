using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MateciucRobert_Lab2.Data;
using MateciucRobert_Lab2.Models;
using MateciucRobert_Lab2.Models.ViewModels;
using System.Security.Policy;

namespace MateciucRobert_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly MateciucRobert_Lab2.Data.MateciucRobert_Lab2Context _context;
        private IList<Models.Publisher> publisher = default!;

        public IndexModel(MateciucRobert_Lab2.Data.MateciucRobert_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get => publisher; set => publisher = value; }
        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();
            PublisherData.Publishers = await _context.Publisher
           .Include(i => i.Books)
           .ThenInclude(c => c.Author)
           .OrderBy(i => i.PublisherName)
           .ToListAsync();

            if (id != null)
            {
                PublisherID = id.value_;  
               Publisher publisher = PublisherData.Publishers
               .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publisher.books;
            }

        }
    }
}
