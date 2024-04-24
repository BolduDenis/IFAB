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
    public class MatchReportsController : Controller
    {
        private readonly IFABDbContext _context;

        public MatchReportsController(IFABDbContext context)
        {
            _context = context;
        }

        // GET: MatchReports
        public async Task<IActionResult> Index()
        {
            var iFABDbContext = _context.MatchReports.Include(m => m.Match);
            return View(await iFABDbContext.ToListAsync());
        }

        // GET: MatchReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchReport = await _context.MatchReports
                .Include(m => m.Match)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (matchReport == null)
            {
                return NotFound();
            }

            return View(matchReport);
        }

        // GET: MatchReports/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId");
            return View();
        }

        // POST: MatchReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,MatchId,StartTime,EndTime,DurationBtwRounds,HalfTimeScore,FinalScore")] MatchReport matchReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", matchReport.MatchId);
            return View(matchReport);
        }

        // GET: MatchReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchReport = await _context.MatchReports.FindAsync(id);
            if (matchReport == null)
            {
                return NotFound();
            }
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", matchReport.MatchId);
            return View(matchReport);
        }

        // POST: MatchReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,MatchId,StartTime,EndTime,DurationBtwRounds,HalfTimeScore,FinalScore")] MatchReport matchReport)
        {
            if (id != matchReport.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchReportExists(matchReport.ReportId))
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
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", matchReport.MatchId);
            return View(matchReport);
        }

        // GET: MatchReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchReport = await _context.MatchReports
                .Include(m => m.Match)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (matchReport == null)
            {
                return NotFound();
            }

            return View(matchReport);
        }

        // POST: MatchReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchReport = await _context.MatchReports.FindAsync(id);
            if (matchReport != null)
            {
                _context.MatchReports.Remove(matchReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchReportExists(int id)
        {
            return _context.MatchReports.Any(e => e.ReportId == id);
        }
    }
}
