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
    public class PagesController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PagesController(MyDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pages
        public async Task<IActionResult> Index()
        {
            return _context.PagesInfo != null ?
                        View(await _context.PagesInfo.ToListAsync()) :
                        Problem("Entity set 'MyDBContext.PagesInfo'  is null.");
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PagesInfo == null)
            {
                return NotFound();
            }

            var pagesInfo = await _context.PagesInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagesInfo == null)
            {
                return NotFound();
            }

            return View(pagesInfo);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,MainImage")] PagesInfo pagesInfo,
            IFormFile FileImage)
        {
            //save captured to server drive location
            // Upload the image file
            //string imageFilePath = @"C:\Path\To\Your\Image.jpg"; // Replace with the actual file path
            string uploadedPath = await UPloadHelper(FileImage);
            pagesInfo.MainImage = uploadedPath;

            ModelState.Clear();
            TryValidateModel(pagesInfo);

            if (ModelState.IsValid)
            {
                _context.Add(pagesInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pagesInfo);
        }

        private async Task<string> UPloadHelper(IFormFile FileImage)
        {

            //File UPload 
            string fileName = FileImage.FileName;

            string destinationPath = Path.Combine(_webHostEnvironment.WebRootPath, "private");

            //if folder exists or not check 
            //if no then we can cretae it also
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            string filePath = Path.Combine(destinationPath, fileName);

            // Save the uploaded file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await FileImage.CopyToAsync(stream);
            }
            return fileName;
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PagesInfo == null)
            {
                return NotFound();
            }

            var pagesInfo = await _context.PagesInfo.FindAsync(id);
            if (pagesInfo == null)
            {
                return NotFound();
            }
            return View(pagesInfo);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,MainImage")] PagesInfo pagesInfo)
        {
            if (id != pagesInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagesInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesInfoExists(pagesInfo.Id))
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
            return View(pagesInfo);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PagesInfo == null)
            {
                return NotFound();
            }

            var pagesInfo = await _context.PagesInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagesInfo == null)
            {
                return NotFound();
            }

            return View(pagesInfo);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PagesInfo == null)
            {
                return Problem("Entity set 'MyDBContext.PagesInfo'  is null.");
            }
            var pagesInfo = await _context.PagesInfo.FindAsync(id);
            if (pagesInfo != null)
            {
                _context.PagesInfo.Remove(pagesInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesInfoExists(int id)
        {
            return (_context.PagesInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
