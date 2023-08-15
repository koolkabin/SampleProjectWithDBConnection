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
    public class CoursesController : Controller
    {
        private readonly MyDBContext _context;

        public CoursesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
              return _context.CoursesInfo != null ? 
                          View(await _context.CoursesInfo.ToListAsync()) :
                          Problem("Entity set 'MyDBContext.CoursesInfo'  is null.");
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CoursesInfo == null)
            {
                return NotFound();
            }

            var coursesInfo = await _context.CoursesInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursesInfo == null)
            {
                return NotFound();
            }

            return View(coursesInfo);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,DurationYears,Id,Title,Content,MainImage")] CoursesInfo coursesInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursesInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coursesInfo);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CoursesInfo == null)
            {
                return NotFound();
            }

            var coursesInfo = await _context.CoursesInfo.FindAsync(id);
            if (coursesInfo == null)
            {
                return NotFound();
            }
            return View(coursesInfo);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,DurationYears,Id,Title,Content,MainImage")] CoursesInfo coursesInfo)
        {
            if (id != coursesInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursesInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesInfoExists(coursesInfo.Id))
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
            return View(coursesInfo);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CoursesInfo == null)
            {
                return NotFound();
            }

            var coursesInfo = await _context.CoursesInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursesInfo == null)
            {
                return NotFound();
            }

            return View(coursesInfo);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CoursesInfo == null)
            {
                return Problem("Entity set 'MyDBContext.CoursesInfo'  is null.");
            }
            var coursesInfo = await _context.CoursesInfo.FindAsync(id);
            if (coursesInfo != null)
            {
                _context.CoursesInfo.Remove(coursesInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesInfoExists(int id)
        {
          return (_context.CoursesInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
