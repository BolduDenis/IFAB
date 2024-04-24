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
    public class TrainingMaterialsController : Controller
    {
        private readonly IFABDbContext _context;

        public TrainingMaterialsController(IFABDbContext context)
        {
            _context = context;
        }

        // GET: TrainingMaterials
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingMaterials.ToListAsync());
        }

        // GET: TrainingMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingMaterial = await _context.TrainingMaterials
                .FirstOrDefaultAsync(m => m.MaterialId == id);
            if (trainingMaterial == null)
            {
                return NotFound();
            }

            return View(trainingMaterial);
        }

        // GET: TrainingMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialId,Title,Description,Content,UploadDate")] TrainingMaterial trainingMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingMaterial);
        }

        // GET: TrainingMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingMaterial = await _context.TrainingMaterials.FindAsync(id);
            if (trainingMaterial == null)
            {
                return NotFound();
            }
            return View(trainingMaterial);
        }

        // POST: TrainingMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialId,Title,Description,Content,UploadDate")] TrainingMaterial trainingMaterial)
        {
            if (id != trainingMaterial.MaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingMaterialExists(trainingMaterial.MaterialId))
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
            return View(trainingMaterial);
        }

        // GET: TrainingMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingMaterial = await _context.TrainingMaterials
                .FirstOrDefaultAsync(m => m.MaterialId == id);
            if (trainingMaterial == null)
            {
                return NotFound();
            }

            return View(trainingMaterial);
        }

        // POST: TrainingMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingMaterial = await _context.TrainingMaterials.FindAsync(id);
            if (trainingMaterial != null)
            {
                _context.TrainingMaterials.Remove(trainingMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingMaterialExists(int id)
        {
            return _context.TrainingMaterials.Any(e => e.MaterialId == id);
        }
    }
}
