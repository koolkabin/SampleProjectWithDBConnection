using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CollegeWebsiteAdmin.Models;

namespace CollegeWebsiteAdmin.Controllers
{
    public class NewsController : Controller
    {
        private readonly MyDBContext _context;

        public NewsController(MyDBContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
              return _context.NewsInfo != null ? 
                          View(await _context.NewsInfo.ToListAsync()) :
                          Problem("Entity set 'MyDBContext.NewsInfo'  is null.");
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewsInfo == null)
            {
                return NotFound();
            }

            var newsInfo = await _context.NewsInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsInfo == null)
            {
                return NotFound();
            }

            return View(newsInfo);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,MainImage")] NewsInfo newsInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsInfo);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewsInfo == null)
            {
                return NotFound();
            }

            var newsInfo = await _context.NewsInfo.FindAsync(id);
            if (newsInfo == null)
            {
                return NotFound();
            }
            return View(newsInfo);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,MainImage")] NewsInfo newsInfo)
        {
            if (id != newsInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsInfoExists(newsInfo.Id))
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
            return View(newsInfo);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewsInfo == null)
            {
                return NotFound();
            }

            var newsInfo = await _context.NewsInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsInfo == null)
            {
                return NotFound();
            }

            return View(newsInfo);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewsInfo == null)
            {
                return Problem("Entity set 'MyDBContext.NewsInfo'  is null.");
            }
            var newsInfo = await _context.NewsInfo.FindAsync(id);
            if (newsInfo != null)
            {
                _context.NewsInfo.Remove(newsInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsInfoExists(int id)
        {
          return (_context.NewsInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
