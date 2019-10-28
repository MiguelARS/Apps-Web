using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.ApiControllers
{
    [Route("api/Careers")]
    [ApiController]
    public class CareersApiController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CareersApiController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/CareersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Career>>> GetCareers()
        {
            return await _context.Careers.ToListAsync();
        }

        // GET: api/CareersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Career>> GetCareer(int id)
        {
            var career = await _context.Careers
                .Include(x=>x.Courses)
                .FirstAsync(x=>x.CareerID==id);

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }
        // GET: api/CareersApi/5/Teachers
        [HttpGet("{id}/Teachers")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetCareerTeachers(int id)
        {
            var career = await _context.Careers
                .Include(x=>x.Teachers)
                .FirstAsync(x=>x.CareerID==id);

            if (career == null)
            {
                return NotFound();
            }

            return career.Teachers.ToList();
        }
        [HttpGet("{id}/Students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetCareerStudents(int id)
        {
            var career = await _context.Careers
                .Include(x=>x.Students)
                .FirstAsync(x=>x.CareerID==id);

            if (career == null)
            {
                return NotFound();
            }

            return career.Students.ToList();
        }

        // PUT: api/CareersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCareer(int id, Career career)
        {
            if (id != career.CareerID)
            {
                return BadRequest();
            }

            _context.Entry(career).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CareersApi
        [HttpPost]
        public async Task<ActionResult<Career>> PostCareer(Career career)
        {
            _context.Careers.Add(career);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCareer", new { id = career.CareerID }, career);
        }
        [HttpPost("{id}/Student")]
        public async Task<ActionResult<Student>> PostCareerStudent(int id, Student student)
        {
            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }
            student.CareerID=id;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        // DELETE: api/CareersApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Career>> DeleteCareer(int id)
        {
            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }

            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();

            return career;
        }

        private bool CareerExists(int id)
        {
            return _context.Careers.Any(e => e.CareerID == id);
        }
    }
}
