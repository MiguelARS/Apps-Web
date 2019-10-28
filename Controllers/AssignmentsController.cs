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
    public class AssignmentsController : Controller
    {
        private readonly SchoolContext _context;

        public AssignmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Assignments.Include(a => a.Campus).Include(a => a.Course).Include(a => a.Teacher);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Campus)
                .Include(a => a.Course)
                .Include(a => a.Teacher)
                .FirstOrDefaultAsync(m => m.AssignmentID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name");
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "ID", "FirstMidName");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentID,CampusID,CourseID,TeacherID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name", assignment.CampusID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", assignment.CourseID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "ID", "FirstMidName", assignment.TeacherID);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name", assignment.CampusID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", assignment.CourseID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "ID", "FirstMidName", assignment.TeacherID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentID,CampusID,CourseID,TeacherID")] Assignment assignment)
        {
            if (id != assignment.AssignmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.AssignmentID))
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
            ViewData["CampusID"] = new SelectList(_context.Campuses, "CampusID", "Name", assignment.CampusID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", assignment.CourseID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "ID", "FirstMidName", assignment.TeacherID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Campus)
                .Include(a => a.Course)
                .Include(a => a.Teacher)
                .FirstOrDefaultAsync(m => m.AssignmentID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentID == id);
        }
    }
}
