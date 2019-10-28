using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class CampusCareersController : Controller
    {
        private readonly SchoolContext _context;

        public CampusCareersController(SchoolContext context)
        {
            _context = context;
        }

        // GET: CampusCareers
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.campusCareers.Include(c => c.Campus).Include(c => c.Career);
            return View(await schoolContext.ToListAsync());
        }

        // GET: CampusCareers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campusCareer = await _context.campusCareers
                .Include(c => c.Campus)
                .Include(c => c.Career)
                .FirstOrDefaultAsync(m => m.CampusID == id);
            if (campusCareer == null)
            {
                return NotFound();
            }

            return View(campusCareer);
        }

        // GET: CampusCareers/Create
        public IActionResult Create()
        {
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name");
            return View();
        }

        // POST: CampusCareers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampusID,CareerID")] CampusCareer campusCareer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campusCareer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name", campusCareer.CampusID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name", campusCareer.CareerID);
            return View(campusCareer);
        }

        // GET: CampusCareers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campusCareer = await _context.campusCareers.FindAsync(id);
            if (campusCareer == null)
            {
                return NotFound();
            }
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name", campusCareer.CampusID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name", campusCareer.CareerID);
            return View(campusCareer);
        }

        // POST: CampusCareers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampusID,CareerID")] CampusCareer campusCareer)
        {
            if (id != campusCareer.CampusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campusCareer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusCareerExists(campusCareer.CampusID))
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
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name", campusCareer.CampusID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name", campusCareer.CareerID);
            return View(campusCareer);
        }

        // GET: CampusCareers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campusCareer = await _context.campusCareers
                .Include(c => c.Campus)
                .Include(c => c.Career)
                .FirstOrDefaultAsync(m => m.CampusID == id);
            if (campusCareer == null)
            {
                return NotFound();
            }

            return View(campusCareer);
        }

        // POST: CampusCareers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campusCareer = await _context.campusCareers.FindAsync(id);
            _context.campusCareers.Remove(campusCareer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampusCareerExists(int id)
        {
            return _context.campusCareers.Any(e => e.CampusID == id);
        }
    }
}
