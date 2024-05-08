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
    public class MatchesController : Controller
    {
        private readonly IFABDbContext _context;

        public MatchesController(IFABDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Referee")]
        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var iFABDbContext = _context.Matches.Include(m => m.Feedback).Include(m => m.User);
            return View(await iFABDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin, Referee")]
        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Feedback)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [Authorize(Roles = "Admin")]
        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,Date,Location,HomeTeam,AwayTeam,UserId")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId", match.MatchId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", match.UserId);
            return View(match);
        }

        [Authorize(Roles = "Admin")]
        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["MatchId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId", match.MatchId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", match.UserId);
            return View(match);
        }

        [Authorize(Roles = "Admin")]
        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchId,Date,Location,HomeTeam,AwayTeam,UserId")] Match match)
        {
            if (id != match.MatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.MatchId))
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
            ViewData["MatchId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId", match.MatchId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", match.UserId);
            return View(match);
        }

        [Authorize(Roles = "Admin")]
        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Feedback)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [Authorize(Roles = "Admin")]
        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
