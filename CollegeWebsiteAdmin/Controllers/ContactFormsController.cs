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
    public class ContactFormsController : Controller
    {
        private readonly MyDBContext _context;

        public ContactFormsController(MyDBContext context)
        {
            _context = context;
        }

        // GET: ContactForms
        public async Task<IActionResult> Index()
        {
              return _context.ContactFormInfo != null ? 
                          View(await _context.ContactFormInfo.ToListAsync()) :
                          Problem("Entity set 'MyDBContext.ContactFormInfo'  is null.");
        }

        // GET: ContactForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactFormInfo == null)
            {
                return NotFound();
            }

            var contactFormInfo = await _context.ContactFormInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactFormInfo == null)
            {
                return NotFound();
            }

            return View(contactFormInfo);
        }

        // GET: ContactForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Address,Email,Mobile,Message")] ContactFormInfo contactFormInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactFormInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactFormInfo);
        }

        // GET: ContactForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactFormInfo == null)
            {
                return NotFound();
            }

            var contactFormInfo = await _context.ContactFormInfo.FindAsync(id);
            if (contactFormInfo == null)
            {
                return NotFound();
            }
            return View(contactFormInfo);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Address,Email,Mobile,Message")] ContactFormInfo contactFormInfo)
        {
            if (id != contactFormInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactFormInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactFormInfoExists(contactFormInfo.Id))
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
            return View(contactFormInfo);
        }

        // GET: ContactForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactFormInfo == null)
            {
                return NotFound();
            }

            var contactFormInfo = await _context.ContactFormInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactFormInfo == null)
            {
                return NotFound();
            }

            return View(contactFormInfo);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactFormInfo == null)
            {
                return Problem("Entity set 'MyDBContext.ContactFormInfo'  is null.");
            }
            var contactFormInfo = await _context.ContactFormInfo.FindAsync(id);
            if (contactFormInfo != null)
            {
                _context.ContactFormInfo.Remove(contactFormInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormInfoExists(int id)
        {
          return (_context.ContactFormInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
