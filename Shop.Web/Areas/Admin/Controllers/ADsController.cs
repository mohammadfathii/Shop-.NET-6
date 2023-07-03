using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Services.Interfaces;
using Shop.Web.Models;
using Shop.Web.Models.ViewModel;
using System.Security.Policy;

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
        public async Task<IActionResult> Create(CreateUpdateAdsViewModel aD)
        {
            var image = "ad";
            if (aD.Thumbnail.Length > 0)
            {
                image = await _service.UploadFile(aD.Thumbnail,@"Images\Ads\");
            }
            var AD = new AD()
            {
                Title = aD.Title,
                URL = aD.URL,
                Thumbnail = image,
                ExpireTime = aD.ExpireTime,
                CategoryId = aD.CategoryId,
            };
            _context.Add(AD);
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
            var ADVM = new CreateUpdateAdsViewModel()
            {
                Id = aD.Id,
                Title = aD.Title,
                Image = aD.Thumbnail,
                CategoryId=aD.CategoryId,
                ExpireTime =aD.ExpireTime,
                URL = aD.URL,
            };
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", aD.CategoryId);
            return View(ADVM);
        }

        // POST: Admin/ADs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateAdsViewModel aD)
        {
            if (id != aD.Id)
            {
               return NotFound();
            }
            var findad = _context.ADs.FirstOrDefault(p => p.Id == id);
            aD.Image = findad.Thumbnail;
            try
            {
                var image = "";
                if (aD.Thumbnail != null && aD.Thumbnail.Length > 0)
                {
                    _service.DeleteFile(@"Images\Ads\" + findad.Thumbnail);
                    image = await _service.UploadFile(aD.Thumbnail,@"Images\Ads\");
                }
                _context.ADs.Update(new AD(){
                    Id = id,
                    Title = aD.Title,
                    URL = aD.URL,
                    Thumbnail = image == "" ? aD.Image : image,
                    ExpireTime = aD.ExpireTime,
                    CategoryId = aD.CategoryId,
                    Created = DateTime.Now,
                });
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
