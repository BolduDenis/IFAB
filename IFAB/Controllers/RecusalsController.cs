using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IFAB.AppDbContext;
using IFAB.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFAB.Controllers
{
    [Authorize]
    public class RecusalsController : Controller
    {
        private readonly IFABDbContext _context;

        public RecusalsController(IFABDbContext context)
        {
            _context = context;
        }

        // GET: Recusals
        public async Task<IActionResult> Index()
        {
            var iFABDbContext = _context.Recusals.Include(r => r.Match).Include(r => r.User);
            return View(await iFABDbContext.ToListAsync());
        }

        // GET: Recusals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recusal = await _context.Recusals
                .Include(r => r.Match)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RecusalId == id);
            if (recusal == null)
            {
                return NotFound();
            }

            return View(recusal);
        }

        // GET: Recusals/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Recusals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecusalId,Reason,Unavailability,MatchId,UserId")] Recusal recusal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recusal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", recusal.MatchId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", recusal.UserId);
            return View(recusal);
        }

        // GET: Recusals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recusal = await _context.Recusals.FindAsync(id);
            if (recusal == null)
            {
                return NotFound();
            }
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", recusal.MatchId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", recusal.UserId);
            return View(recusal);
        }

        // POST: Recusals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecusalId,Reason,Unavailability,MatchId,UserId")] Recusal recusal)
        {
            if (id != recusal.RecusalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recusal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecusalExists(recusal.RecusalId))
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
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", recusal.MatchId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", recusal.UserId);
            return View(recusal);
        }

        // GET: Recusals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recusal = await _context.Recusals
                .Include(r => r.Match)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RecusalId == id);
            if (recusal == null)
            {
                return NotFound();
            }

            return View(recusal);
        }

        // POST: Recusals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recusal = await _context.Recusals.FindAsync(id);
            if (recusal != null)
            {
                _context.Recusals.Remove(recusal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecusalExists(int id)
        {
            return _context.Recusals.Any(e => e.RecusalId == id);
        }
    }
}
