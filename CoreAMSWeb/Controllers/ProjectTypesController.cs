using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreAMSWeb.Data;
using CoreAMSWeb.Models;

namespace CoreAMSWeb.Controllers
{
    public class ProjectTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTypes
        public async Task<IActionResult> Index()
        {
              return _context.ProjectType != null ? 
                          View(await _context.ProjectType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProjectType'  is null.");
        }

        // GET: ProjectTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectType == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // GET: ProjectTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectName")] ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectType);
        }

        // GET: ProjectTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectType == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectType.FindAsync(id);
            if (projectType == null)
            {
                return NotFound();
            }
            return View(projectType);
        }

        // POST: ProjectTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectName")] ProjectType projectType)
        {
            if (id != projectType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTypeExists(projectType.Id))
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
            return View(projectType);
        }

        // GET: ProjectTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectType == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // POST: ProjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProjectType'  is null.");
            }
            var projectType = await _context.ProjectType.FindAsync(id);
            if (projectType != null)
            {
                _context.ProjectType.Remove(projectType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTypeExists(int id)
        {
          return (_context.ProjectType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
