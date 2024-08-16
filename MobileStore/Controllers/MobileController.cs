using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Controllers
{
    public class MobilesController : Controller
    {
        private readonly MobileContext _context;

        public MobilesController(MobileContext context)
        {
            _context = context;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index()
        {
            // Retrieve all mobiles from the database and pass them to the view
            return View(await _context.Mobiles.ToListAsync());
        }

        // GET: Mobiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the mobile with the specified id
            var mobile = await _context.Mobiles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mobile == null)
            {
                return NotFound();
            }

            // Pass the mobile object to the view for displaying details
            return View(mobile);
        }

        // GET: Mobiles/Create
        public IActionResult Create()
        {
            // Display the form for creating a new mobile
            return View();
        }

        // POST: Mobiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Brand,Price,Stock")] Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mobile);
        }

        // GET: Mobiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the mobile with the specified id
            var mobile = await _context.Mobiles.FindAsync(id);
            if (mobile == null)
            {
                return NotFound();
            }

            // Display the form for editing the mobile
            return View(mobile);
        }

        // POST: Mobiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Price,Stock")] Mobile mobile)
        {
            if (id != mobile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileExists(mobile.Id))
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
            return View(mobile);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the mobile with the specified id
            var mobile = await _context.Mobiles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mobile == null)
            {
                return NotFound();
            }

            // Display the confirmation page for deleting the mobile
            return View(mobile);
        }

        // POST: Mobiles/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mobile = await _context.Mobiles.FindAsync(id);
            if (mobile == null)
            {
                return NotFound();
            }

            _context.Mobiles.Remove(mobile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MobileExists(int id)
        {
            return _context.Mobiles.Any(e => e.Id == id);
        }
    }
}
