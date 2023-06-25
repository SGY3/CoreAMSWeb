using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreAMSWeb.Data;
using CoreAMSWeb.Models;
using CoreAMSWeb.ViewModels;
using System.Diagnostics;

namespace CoreAMSWeb.Controllers
{
    public class ActivityMastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityMastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivityMasters
        public async Task<IActionResult> Index()
        {

            return _context.ActivityMaster != null ?
                        View(await _context.ActivityMaster.Include(x => x.ActivityName).Include(x => x.ProjectName).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ActivityMaster'  is null.");
        }

        // GET: ActivityMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActivityMaster == null)
            {
                return NotFound();
            }

            var activityMaster = await _context.ActivityMaster.Include(x => x.ActivityName).Include(x => x.ProjectName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityMaster == null)
            {
                return NotFound();
            }

            return View(activityMaster);
        }

        // GET: ActivityMasters/Create
        public IActionResult Create()
        {
            var activityTypeList = _context.ActivityType.ToList();
            var projectTypeList = _context.ProjectType.ToList();
            var vm = new AddActivity
            {
                ActivityName = activityTypeList.Select(d => new SelectListItem
                {
                    Text = d.ActivityName,
                    Value = d.Id.ToString()
                }).ToList(),

                ProjectName = projectTypeList.Select(d => new SelectListItem
                {
                    Text = d.ProjectName,
                    Value = d.Id.ToString()
                }).ToList()
            };

            return View(vm);
        }

        // POST: ActivityMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddActivity activityData)
        {
            if (ModelState.IsValid)
            {
                var activity = new ActivityMaster
                {
                    ProjectNameId = activityData.SelectedProjectId,
                    Title = activityData.Title,
                    Description = activityData.Description,
                    ActivityNameId = activityData.SelectedActivityId,
                    PageName = activityData.PageName,
                    StoredProcedureName = activityData.StoredProcedureName,
                    AddedOn = DateTime.Now
                };
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: ActivityMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActivityMaster == null)
            {
                return NotFound();
            }

            var activityMaster = await _context.ActivityMaster.FindAsync(id);
            if (activityMaster == null)
            {
                return NotFound();
            }
            var activityTypeList = _context.ActivityType.ToList();
            var projectTypeList = _context.ProjectType.ToList();
            var vm = new AddActivity
            {
                Title = activityMaster.Title,
                Description = activityMaster.Description,
                PageName = activityMaster.PageName,
                StoredProcedureName = activityMaster.StoredProcedureName,
                AddedOn = activityMaster.AddedOn,
                ActivityName = activityTypeList.Select(d => new SelectListItem
                {
                    Text = d.ActivityName,
                    Value = d.Id.ToString()
                }).ToList(),

                ProjectName = projectTypeList.Select(d => new SelectListItem
                {
                    Text = d.ProjectName,
                    Value = d.Id.ToString()
                }).ToList(),
                SelectedActivityId = activityMaster.ActivityNameId,
                SelectedProjectId = activityMaster.ProjectNameId
            };
            vm.ActivityName.Find(x => x.Value == activityMaster.ActivityNameId.ToString()).Selected = true;
            vm.ProjectName.Find(x => x.Value == activityMaster.ProjectNameId.ToString()).Selected = true;
            return View(vm);
        }

        // POST: ActivityMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddActivity activityData)
        {
            if (id != activityData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var activity = new ActivityMaster
                    {
                        Id = activityData.Id,
                        ProjectNameId = activityData.SelectedProjectId,
                        Title = activityData.Title,
                        Description = activityData.Description,
                        ActivityNameId = activityData.SelectedActivityId,
                        PageName = activityData.PageName,
                        StoredProcedureName = activityData.StoredProcedureName,
                        AddedOn = activityData.AddedOn,
                        ModifiedOn = DateTime.Now
                    };

                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityMasterExists(activityData.Id))
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
            return View();
        }

        // GET: ActivityMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActivityMaster == null)
            {
                return NotFound();
            }

            var activityMaster = await _context.ActivityMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityMaster == null)
            {
                return NotFound();
            }

            return View(activityMaster);
        }

        // POST: ActivityMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActivityMaster == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ActivityMaster'  is null.");
            }
            var activityMaster = await _context.ActivityMaster.FindAsync(id);
            if (activityMaster != null)
            {
                _context.ActivityMaster.Remove(activityMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityMasterExists(int id)
        {
            return (_context.ActivityMaster?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
