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
    public class ToDoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDo.Include(t => t.ActivityName).Include(t => t.ProjectName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDo == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityName)
                .Include(t => t.ProjectName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            ViewData["ActivityNameId"] = new SelectList(_context.ActivityType, "Id", "ActivityName");
            ViewData["ProjectNameId"] = new SelectList(_context.ProjectType, "Id", "ProjectName");
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,PageName,StoredProcedureName,AddedOn,ProjectNameId,ActivityNameId,ModifiedOn")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityNameId"] = new SelectList(_context.ActivityType, "Id", "ActivityName", toDo.ActivityNameId);
            ViewData["ProjectNameId"] = new SelectList(_context.ProjectType, "Id", "ProjectName", toDo.ProjectNameId);
            return View(toDo);
        }

        // GET: ToDoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDo == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            ViewData["ActivityNameId"] = new SelectList(_context.ActivityType, "Id", "ActivityName", toDo.ActivityNameId);
            ViewData["ProjectNameId"] = new SelectList(_context.ProjectType, "Id", "ProjectName", toDo.ProjectNameId);
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PageName,StoredProcedureName,AddedOn,ProjectNameId,ActivityNameId,ModifiedOn")] ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    toDo.ModifiedOn = DateTime.Now;
                    _context.Update(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.Id))
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
            ViewData["ActivityNameId"] = new SelectList(_context.ActivityType, "Id", "ActivityName", toDo.ActivityNameId);
            ViewData["ProjectNameId"] = new SelectList(_context.ProjectType, "Id", "ProjectName", toDo.ProjectNameId);
            return View(toDo);
        }

        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDo == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityName)
                .Include(t => t.ProjectName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToDo'  is null.");
            }
            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo != null)
            {
                _context.ToDo.Remove(toDo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            return (_context.ToDo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
