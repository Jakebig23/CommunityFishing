using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityFishing.Models;

namespace CommunityFishing.Controllers
{
    public class FishInfoesController : Controller
    {
        private readonly CommunityFishingContext _context;

        public FishInfoesController(CommunityFishingContext context)
        {
            _context = context;
        }

        // GET: FishInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FishInfo.ToListAsync());
        }

        // GET: FishInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishInfo = await _context.FishInfo
                .FirstOrDefaultAsync(m => m.FishId == id);
            if (fishInfo == null)
            {
                return NotFound();
            }

            return View(fishInfo);
        }

        // GET: FishInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FishInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FishId,FishName,LegalLength,LegalLimit,SeasonStart,SeasonEnd")] FishInfo fishInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fishInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fishInfo);
        }

        // GET: FishInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishInfo = await _context.FishInfo.FindAsync(id);
            if (fishInfo == null)
            {
                return NotFound();
            }
            return View(fishInfo);
        }

        // POST: FishInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FishId,FishName,LegalLength,LegalLimit,SeasonStart,SeasonEnd")] FishInfo fishInfo)
        {
            if (id != fishInfo.FishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fishInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishInfoExists(fishInfo.FishId))
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
            return View(fishInfo);
        }

        // GET: FishInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishInfo = await _context.FishInfo
                .FirstOrDefaultAsync(m => m.FishId == id);
            if (fishInfo == null)
            {
                return NotFound();
            }

            return View(fishInfo);
        }

        // POST: FishInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fishInfo = await _context.FishInfo.FindAsync(id);
            _context.FishInfo.Remove(fishInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FishInfoExists(int id)
        {
            return _context.FishInfo.Any(e => e.FishId == id);
        }
    }
}
