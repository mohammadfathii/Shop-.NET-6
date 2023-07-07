using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Services.Interfaces;
using Shop.Web.Models;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ADsController : Controller
    {
        private readonly ShopDBContext _context;
        public IServerSideService _service;

        public ADsController(ShopDBContext context,IServerSideService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Admin/ADs
        public async Task<IActionResult> Index()
        {
            var shopDBContext = _context.ADs.Include(a => a.Category);
            return View(await shopDBContext.ToListAsync());
        }

        // GET: Admin/ADs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ADs == null)
            {
                return NotFound();
            }

            var aD = await _context.ADs
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aD == null)
            {
                return NotFound();
            }

            return View(aD);
        }

        // GET: Admin/ADs/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/ADs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AD aD)
        {
            var image = "ad";
            if (aD.formFile.Length > 0)
            {
                image = await _service.UploadFile(aD.formFile,@"Images\Ads\");
            }
            aD.Thumbnail = image;
            _context.Add(aD);
            await _context.SaveChangesAsync();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", aD.CategoryId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ADs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.ADs == null)
            {
                return NotFound();
            }
            var aD = await _context.ADs.FindAsync(id);
            if (aD == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", aD.CategoryId);
            return View(aD);
        }

        // POST: Admin/ADs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AD aD)
        {
            if (id != aD.Id)
            {
                return NotFound();
            }

            try
            {
                var existingAd = _context.ADs.FirstOrDefault(p => p.Id == id);

                if (existingAd == null)
                {
                    return NotFound();
                }

                var image = "";
                if (aD.Thumbnail != null && aD.Thumbnail.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingAd.Thumbnail))
                    {
                        _service.DeleteFile(@"Images\Ads\" + existingAd.Thumbnail);
                    }
                    image = await _service.UploadFile(aD.formFile, @"Images\Ads\");
                }
                existingAd.Title = aD.Title;
                existingAd.URL = aD.URL;
                existingAd.Thumbnail = image == "" ? existingAd.Thumbnail : image;
                existingAd.ExpireTime = aD.ExpireTime;

                _context.Update(existingAd);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ADExists(aD.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", aD.CategoryId);
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/ADs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ADs == null)
            {
                return NotFound();
            }

            var aD = await _context.ADs
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aD == null)
            {
                return NotFound();
            }

            return View(aD);
        }

        // POST: Admin/ADs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ADs == null)
            {
                return Problem("Entity set 'ShopDBContext.ADs'  is null.");
            }
            var aD = await _context.ADs.FindAsync(id);
            if (aD != null)
            {
                _service.DeleteFile(@"Images\Ads\" + aD.Thumbnail);
                _context.ADs.Remove(aD);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADExists(int id)
        {
            return (_context.ADs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
