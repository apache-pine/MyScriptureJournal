using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournalContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "BookAsc";

        public IList<Scripture> Scripture { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ScriptureBook { get; set; }

        public async Task OnGetAsync(string sortOrder, string sortBy, string searchString, string scriptureBook)
        {
            IQueryable<string> bookQuery = from s in _context.Scripture
                                           orderby s.Book
                                           select s.Book;

            var scriptures = from s in _context.Scripture
                             select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(scriptureBook))
            {
                scriptures = scriptures.Where(s => s.Book == scriptureBook);
            }

            ViewData["SortOrder"] = sortOrder;
            ViewData["SortBy"] = sortBy;

            switch (sortBy)
            {
                case "BookAsc":
                    scriptures = scriptures.OrderBy(s => s.Book);
                    break;
                case "BookDesc":
                    scriptures = scriptures.OrderByDescending(s => s.Book);
                    break;
                case "EntryDateAsc":
                    scriptures = scriptures.OrderBy(s => s.EntryDate);
                    break;
                case "EntryDateDesc":
                    scriptures = scriptures.OrderByDescending(s => s.EntryDate);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.Book);
                    break;
            }

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Scripture = await scriptures.ToListAsync();
        }
    }
}
